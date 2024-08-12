import React, { useState, useEffect } from 'react';
import Remove from '../../assets/remove.png';
import './friends.css';

function Friends({ isOpen }) { 
  const [friends, setFriends] = useState([]);
  const [hasFriends, setHasFriends] = useState(true); // New state to track if the user has friends

  useEffect(() => {
    // Fetch friends data from the backend API
    const fetchFriends = async () => {
      try {
        const response = await fetch('https://localhost:7245/api/UserFriends/GetFriends', {
          method: 'GET'
        });

        if (response.ok) {
          const data = await response.json();
          console.log('Fetched Friends:', data); // Log the data
          setFriends(data); // Set the fetched friends data to the state
        } else if (response.status === 404) { 
          // If no friends are found, set hasFriends to false
          setHasFriends(false);
        } else {
          console.error('Failed to fetch friends');
        }
      } catch (error) {
        console.error('Error:', error);
      }
    };

    fetchFriends();
  }, []);

  return (
    <div className="friends-container-main">
      <h1 className='friends-title'>
        {hasFriends ? `Your Friends (${friends.length}):` : 'You have no friends'}
      </h1>
      <div className="friend-separator"></div>
      {hasFriends && (
        <div className='friends-container'>
          {
            friends.map((user) => (
              <div className='friend-item' key={user.id}>
                <img src={user.pfp} alt='' className='friend-item-pfp' />
                <div className='friend-item-details-container'>
                  <h2 className='friend-item-username'>
                    {user.username}
                    <p className="friend-status"> • {user.status}</p>
                  </h2>
                </div>
                <button className={isOpen ? 'friend-item-btn-open' : 'friend-item-btn-close'}>
                  <img src={Remove} alt="" className="friend-item-btn-icon" />
                  <p className="friend-btnText">Remove</p>
                </button>
              </div>
            ))
          }
        </div>
      )}
    </div>
  );
}

export default Friends;
