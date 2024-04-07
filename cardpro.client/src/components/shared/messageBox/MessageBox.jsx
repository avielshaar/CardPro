import React from 'react';
import Alert from 'react-bootstrap/Alert';
import './MessageBox.css';

const MessageBox = ({ message, isError }) => {
  const backgroundColor = isError ? '#c22431' : '#2ecc71';
  return (
    <Alert variant='danger' className='error-alert' style={{ backgroundColor: backgroundColor }}>
      {message}
    </Alert>
  );
};

export default MessageBox;
