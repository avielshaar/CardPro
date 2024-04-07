import React, { useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import MessageBox from '../../components/shared/messageBox/MessageBox.jsx';
import axios from 'axios';
import Logo from '../../components/shared/logo/Logo.jsx';
import './LoginPage.css';

const LoginPage = () => {
  const [error, setError] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const navigate = useNavigate();
  const { search } = useLocation();
  const redirectUrl = new URLSearchParams(search);
  const redirectValue = redirectUrl.get('redirect');
  const redirect = redirectValue ? redirectValue : '/search';

  const submitHandler = async (e) => {
    e.preventDefault();
    try {
      const { data } = await axios.post('api/login', {
        email: email,
        password: password,
      });
      localStorage.setItem('token', JSON.stringify(data));
      navigate(redirect);
    } catch (error) {
      setError(error.response.data);
    }
  };

  const autoFillHandler = (e) => {
    e.preventDefault();
    setEmail('admin@gmail.com');
    setPassword('12345678');
  };

  return (
    <div className='login-page'>
      <Logo />
      <form onSubmit={submitHandler}>
        <div className='input-group'>
          <input type='text' required value={email} onChange={(e) => setEmail(e.target.value)} placeholder='Email' />
          <input type='password' required value={password} onChange={(e) => setPassword(e.target.value)} placeholder='Password' />
          <button type='submit' className='btn'>
            Log In
          </button>
          <br />
          <button onClick={(e) => autoFillHandler(e)} className='btn'>
            Press here to enter as test user
          </button>
          <br />
          {error !== '' ? (
            <div className='error-message'>
              <br />
              <MessageBox message={error} isError={true} />
            </div>
          ) : (
            ''
          )}
        </div>
      </form>
    </div>
  );
};

export default LoginPage;
