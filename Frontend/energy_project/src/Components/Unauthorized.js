import React, { useState } from 'react';
import ExitChatButton from './ExitChatButton';

const Unauthorized = () => {
  return (
    <div>
      <h1>Unauthorized</h1>
      <ExitChatButton />
    </div>
  );
}

export default Unauthorized;
