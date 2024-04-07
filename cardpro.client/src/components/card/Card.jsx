import React, { useEffect, useState } from 'react';
import axios from 'axios';
import './Card.css';

const Card = ({ card, onClick }) => {
  const clickHandler = () => {
    onClick(card);
  };

  const numberSplitter = () => {
    const parts = [];
    for (let i = 0; i < card.number.length; i += 4) {
      parts.push(card.number.substring(i, i + 4));
    }
    return parts[0] + ' ' + parts[1] + ' ' + parts[2] + ' ' + parts[3];
  };

  return (
    <div className='card' onClick={clickHandler}>
      <div className='card-content'>
        <img className='card-image' src={`${axios.defaults.baseURL}${card.imagePath}`} alt={card.number} />
        <div className='card-details'>
          <p className='card-bank'>{card.bankName}</p>
          <h4 className='card-number'>{numberSplitter()}</h4>
        </div>
      </div>
    </div>
  );
};

export default Card;
