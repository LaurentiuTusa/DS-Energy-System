const localServerUser = 'https://localhost:7167';
const localServerDevice = 'https://localhost:7172';
const localServerChat = 'https://localhost:7215/chat';
const dockerServerUser = 'http://host.docker.internal:8082';
const dockerServerDevice = 'http://host.docker.internal:8083';
const dockerServerChat = 'http://host.docker.internal:8085/chat';

const ServerSelector = {
  localServerUser,
  localServerDevice,
  dockerServerUser,
  dockerServerDevice,
  localServerChat,
  dockerServerChat,

  userServer: localServerUser, // Default to local server for users
  deviceServer: localServerDevice, // Default to local server for devices
  chatServer: localServerChat // Default to local server for chat service
};

export default ServerSelector;
