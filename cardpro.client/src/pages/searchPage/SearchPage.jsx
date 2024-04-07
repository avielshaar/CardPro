import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import Card from '../../components/card/Card.jsx';
import Loading from '../../components/shared/loading/Loading.jsx';
import MessageBox from '../../components/shared/messageBox/MessageBox.jsx';
import Logo from '../../components/shared/logo/Logo.jsx';
import './SearchPage.css';

const SearchPage = () => {
  const [error, setError] = useState('');
  const [banks, setBanks] = useState([]);
  const [cards, setCards] = useState([]);
  const [isBlocked, setIsBlocked] = useState('');
  const [cardNumber, setCardNumber] = useState('');
  const [bankCode, setBankCode] = useState('');
  const [loading, setLoading] = useState(false);
  const token = JSON.parse(localStorage.getItem('token'));
  const navigate = useNavigate();

  const getBanks = async () => {
    setLoading(true);
    try {
      const { data } = await axios.get('api/banks', {
        headers: { authorization: `Bearer ${token}` },
      });
      setBanks(data);
      setError('');
    } catch (error) {
      setError(error.response.message);
    }
    setLoading(false);
  };

  const getCards = async () => {
    setLoading(true);
    try {
      let url = 'api/cards?';
      if (isBlocked !== '') {
        url += `isBlocked=${isBlocked}&`;
      }
      if (cardNumber !== '') {
        url += `cardNumber=${cardNumber}&`;
      }
      if (bankCode !== '') {
        url += `bankCode=${bankCode}`;
      }
      const { data } = await axios.get(url, {
        headers: { authorization: `Bearer ${token}` },
      });
      setCards(data);
      setError('');
    } catch (error) {
      setError(error.response.data);
      setCards([]);
    }
    setLoading(false);
  };

  const getCardData = (card) => {
    return {
      number: card.number,
      imagePath: card.imagePath,
      bankName: banks.find((b) => b.code === card.bankCode).name,
    };
  };

  const submitHandler = (e) => {
    e.preventDefault();
    getCards();
  };

  const cardClickHandler = (card) => {
    navigate(`/increase/${card.number}`, { state: { card } });
  };

  useEffect(() => {
    getBanks();
    getCards();
  }, []);

  return (
    <div className='search-page'>
      <Logo />
      <br/>
      {loading ? (
        <Loading />
      ) : (
        <>
          <div className='search-form'>
            <form onSubmit={submitHandler}>
              <div className='radio-buttons'>
                <label className='radio-label'>
                  <input type='radio' name='isBlocked' checked={isBlocked === ''} onChange={() => setIsBlocked('')} />
                  All cards
                </label>
                <label className='radio-label'>
                  <input type='radio' name='isBlocked' checked={isBlocked === 'true'} onChange={() => setIsBlocked('true')} />
                  Blocked cards
                </label>
                <label className='radio-label'>
                  <input type='radio' name='isBlocked' checked={isBlocked === 'false'} onChange={() => setIsBlocked('false')} />
                  Not blocked cards
                </label>
              </div>
              <div className='text-inputs'>
                <input type='text' value={cardNumber} onChange={(e) => setCardNumber(e.target.value)} placeholder='Card number' />
                <input type='text' value={bankCode} onChange={(e) => setBankCode(e.target.value)} placeholder='BankCode' />
              </div>
              <button type='submit' className='btn'>
                Search
              </button>
            </form>
          </div>
          <div className='search-results'>
            {error ? (
              <div className='error-message'>
                <br />
                <MessageBox message={error} isError={true} />
              </div>
            ) : (
              <div className='cards'>
                {cards.map((card) => (
                  <div key={card.number}>
                    <Card card={getCardData(card)} onClick={cardClickHandler} />
                  </div>
                ))}
              </div>
            )}
          </div>
        </>
      )}
    </div>
  );
};

export default SearchPage;
