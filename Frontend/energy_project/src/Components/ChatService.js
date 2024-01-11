import { useState } from 'react';
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import Lobby from './Lobby';
import Chat from './Chat';
import ExitChatButton from './ExitChatButton';
import '../App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import ServerSelector from './ServerSelector';

const ChatService = () => {
  const [connection, setConnection] = useState();
  const [messages, setMessages] = useState([]);
  const [users, setUsers] = useState([]);

  const joinRoom = async (user, room) => {
    try {
      const connection = new HubConnectionBuilder()
        //.withUrl("https://localhost:7215/chat")
        .withUrl(ServerSelector.chatServer)
        .configureLogging(LogLevel.Information)
        .build();

      connection.on("ReceiveMessage", (user, message) => {
        setMessages(messages => [...messages, { user, message }]);
      });

      connection.on("UsersInRoom", (users) => {
        setUsers(users);
      });

      connection.onclose(e => {
        setConnection();
        setMessages([]);
        setUsers([]);
      });

      await connection.start();
      await connection.invoke("JoinRoom", { user, room });
      setConnection(connection);
    } catch (e) {
      console.log(e);
    }
  }

  const sendMessage = async (message) => {
    try {
      await connection.invoke("SendMessage", message);
    } catch (e) {
      console.log(e);
    }
  }

  const closeConnection = async () => {
    try {
      await connection.stop();
    } catch (e) {
      console.log(e);
    }
  }

  return <div className='app'>
      <h2>MyChat</h2>
      <hr className='line' />
      {!connection
        ? <div>
            <ExitChatButton />
            <Lobby joinRoom={joinRoom} />
          </div>
        : <Chat sendMessage={sendMessage} messages={messages} users={users} closeConnection={closeConnection} />}
    </div>


}

export default ChatService;
