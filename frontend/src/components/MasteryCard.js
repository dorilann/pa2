
import React from 'react';
import '../styles/MasteryCard.css';

const MasteryCard = ({ champion }) => {
  return (
    <div className="mastery-card">
      <img
        src={`https://ddragon.leagueoflegends.com/cdn/11.24.1/img/champion/${champion.id}.png`}
        alt={champion.name}
        className="champion-image"
      />
      <h3>{champion.name}</h3>
      <p>{champion.title}</p>
      <p><strong>Tags:</strong> {champion.tags.join(', ')}</p>
      <p>{champion.blurb}</p>
    </div>
  );
};

export default MasteryCard;
