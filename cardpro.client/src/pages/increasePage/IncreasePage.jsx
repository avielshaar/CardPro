import React, { useEffect, useState } from 'react';
import MessageBox from '../../components/shared/messageBox/MessageBox.jsx';
import { useLocation } from 'react-router-dom';
import axios from 'axios';
import Logo from '../../components/shared/logo/Logo.jsx';

import './IncreasePage.css';

const IncreasePage = () => {
  const location = useLocation();
  const { card } = location.state;
  const [error, setError] = useState('');
  const [response, setResponse] = useState('');
  const [requestedCreditLimit, setRequestedCreditLimit] = useState('');
  const [employmentType, setEmploymentType] = useState('employed');
  const [averageMonthlyIncome, setAverageMonthlyIncome] = useState('');
  const token = JSON.parse(localStorage.getItem('token'));

  const submitHandler = async (e) => {
    e.preventDefault();
    try {
      const { data } = await axios.put('api/cards', null, {
        params: {
          cardNumber: card.number,
          requestedCreditLimit: requestedCreditLimit,
          employmentType: employmentType,
          averageMonthlyIncome: averageMonthlyIncome,
        },
        headers: { authorization: `Bearer ${token}` },
      });
      setResponse(data);
      setError('');
    } catch (error) {
      setError(error.response.data);
      setResponse('');
    }
  };

  useEffect(() => {
    console.log(card.number);
  }, []);

  return (
    <div className='increase-page'>
      <Logo />
      <div className='increase-form'>
        <form onSubmit={submitHandler}>
          <div className='radio-buttons'>
            <label className='radio-label'>
              <input type='radio' checked={employmentType === 'employed'} onChange={() => setEmploymentType('employed')} />
              <span>Employed</span>
            </label>
            <label className='radio-label'>
              <input type='radio' checked={employmentType === 'self employed'} onChange={() => setEmploymentType('self employed')} />
              <span>Self employed</span>
            </label>
            <label className='radio-label'>
              <input type='radio' checked={employmentType === 'other'} onChange={() => setEmploymentType('other')} />
              <span>Other</span>
            </label>
          </div>
          <br />
          <div className='text-inputs'>
            <input type='text' value={requestedCreditLimit} onChange={(e) => setRequestedCreditLimit(e.target.value)} placeholder='Requested credit limit' />
            <input type='text' value={averageMonthlyIncome} onChange={(e) => setAverageMonthlyIncome(e.target.value)} placeholder='Average monthly income' />
          </div>
          {card.isBlocked ? (
            <div className='blocked-message'>
              <MessageBox message='Card is blocked' isError={true} />
            </div>
          ) : (
            <div className='mb-3'>
              <button type='submit' className='btn'>
                Increase
              </button>
            </div>
          )}
          <br />
          {error !== '' && <MessageBox message={error} isError={true} />}
          {response !== '' && <MessageBox message={response} isError={false} />}
        </form>
      </div>
    </div>
  );
};

export default IncreasePage;
