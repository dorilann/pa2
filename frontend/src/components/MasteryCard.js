const MasteryCard = ({ champion }) => {
  return (
    <div className="mastery-card">
      <h3>{champion.name}</h3>
      <p>Level: {champion.level}</p>
      <p>Points: {champion.points}</p>
    </div>
  );
};

export default MasteryCard;
