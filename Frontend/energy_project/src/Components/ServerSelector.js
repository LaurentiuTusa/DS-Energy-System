const localServerUser = 'https://localhost:7167';
const localServerDevice = 'https://localhost:7172';
const dockerServerUser = 'http://host.docker.internal:8082';
const dockerServerDevice = 'http://host.docker.internal:8083';

const ServerSelector = {
  localServerUser,
  localServerDevice,
  dockerServerUser,
  dockerServerDevice,

  userServer: localServerUser, // Default to local server for users
  deviceServer: localServerDevice // Default to local server for devices
};

export default ServerSelector;
