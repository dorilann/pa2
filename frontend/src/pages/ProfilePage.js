import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import Header from '../components/Header';
import MasteryCard from '../components/MasteryCard';
import '../styles/ProfilePage.css';

const ProfilePage = () => {
  const { region, server, username, tag } = useParams();
  const [profileData, setProfileData] = useState(null);
  const [topChampions, setTopChampions] = useState([]);

  useEffect(() => {
    // Имитация загрузки данных профиля и топ-5 персонажей
    const fetchData = async () => {
      const profileResponse = await fetchProfileData(region, server, username, tag);
      const championsResponse = await fetchTopChampions(region, server, username);
      
      setProfileData(profileResponse);
      setTopChampions(championsResponse);
    };

    fetchData();
  }, [region, server, username, tag]);

  // Функция-заглушка для получения данных профиля
  const fetchProfileData = async () => {
    return {
      rank: 'Unranked',
      level: 158,
      winrate: '0.0%',
      gamesPlayed: 0,
    };
  };

  // Функция-заглушка для получения данных топ-5 персонажей
  const fetchTopChampions = async () => {
    return [
      {
        id: 'Yone',
        name: 'Yone',
        title: 'the Unforgotten',
        tags: ['Fighter', 'Assassin'],
        blurb: 'In life, he was Yone...',
      },
      {
        id: 'Ahri',
        name: 'Ahri',
        title: 'the Nine-Tailed Fox',
        tags: ['Mage', 'Assassin'],
        blurb: 'Innately connected to the latent...',
      },
      {
        id: 'Ezreal',
        name: 'Ezreal',
        title: 'the Prodigal Explorer',
        tags: ['Marksman', 'Mage'],
        blurb: 'A dashing adventurer...',
      },
      {
        id: 'Lux',
        name: 'Lux',
        title: 'the Lady of Luminosity',
        tags: ['Mage', 'Support'],
        blurb: 'Raised in the high walls of...',
      },
      {
        id: 'Garen',
        name: 'Garen',
        title: 'the Might of Demacia',
        tags: ['Fighter', 'Tank'],
        blurb: 'A proud and noble warrior...',
      }
    ];
  };

  if (!profileData) {
    return <div>Loading...</div>;
  }

  return (
    <div>
      <Header />
      <div className="profile-header">
        <h1>{username}#{tag} ({server.toUpperCase()})</h1>
        <p>Level {profileData.level}</p>
        <div className="profile-rating">
          <div>
            <strong>Rank:</strong> {profileData.rank}
          </div>
          <div>
            <strong>Winrate:</strong> {profileData.winrate}
          </div>
          <div>
            <strong>Games Played:</strong> {profileData.gamesPlayed}
          </div>
        </div>
      </div>

      <div className="champions-section">
        <h2>Мастерство </h2>
        <div className="champions-list">
          {topChampions.map((champion) => (
            <MasteryCard key={champion.id} champion={champion} />
          ))}
        </div>
      </div>
    </div>
  );
};

export default ProfilePage;
