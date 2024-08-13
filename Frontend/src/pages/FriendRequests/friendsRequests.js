import React, { useState, useEffect } from 'react';
import './friendsRequests.css';
import { useNavigate } from 'react-router-dom';
import Accept from '../../assets/accept.png';
import Remove from '../../assets/reject.png';

function FriendRequests({ isOpen }) {
  const navigate = useNavigate();
  const userId = localStorage.getItem('userId');
  const [friendsRQ, setFriendRQList] = useState([]);
  const [hasFriendRQ, setHasFriendRQ] = useState(true);

  const fetchFriendsRQ = async () => {
    try {
      const response = await fetch(`https://localhost:7245/api/UserFriends/FriendRequests/${userId}`, {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
        },
      });

      if (response.ok) {
        const data = await response.json();
        setFriendRQList(data);
        setHasFriendRQ(data.length > 0);
        console.log(data);
      } else if (response.status === 404) {
        setHasFriendRQ(false);
      } else {
        console.error('Failed to fetch friends');
      }
    } catch (error) {
      console.log('Error: ' + error);
    }
  };

  useEffect(() => {
    fetchFriendsRQ();
  }, [userId]);

  const acceptRequest = async (fromId, toId) => {
    try {
      const response = await fetch(`https://localhost:7245/api/UserFriends/AcceptRQ/${fromId}/${toId}`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
      });

      if (response.ok) {
        console.log('Friend request accepted');
        window.location.reload();  // Refresh the page after accepting
      } else {
        console.error('Failed to accept friend request');
      }
    } catch (error) {
      console.log('Error: ' + error);
    }
  };

  const rejectRequest = async (fromId, toId) => {
    try {
      const response = await fetch(`https://localhost:7245/api/UserFriends/DeleteRQ/${fromId}/${toId}`, {
        method: 'DELETE',
        headers: {
          'Content-Type': 'application/json',
        },
      });

      if (response.ok) {
        console.log('Friend request rejected');
        window.location.reload();  // Refresh the page after accepting
      } else {
        console.error('Failed to reject friend request');
      }
    } catch (error) {
      console.log('Error: ' + error);
    }
  };

  return (
    <div className="requests-container-main">
      <h1 className='requests-title'>
        {hasFriendRQ ? `Friend Requests: (${friendsRQ.length}):` : 'You Have No Friend Requests'}
      </h1>
      <div className="requests-separator"></div>
      <div className='requests-container'>
        {friendsRQ.map((user) => (
          <div className='requests-item' key={user.id}>
            <img src={`data:image/png;base64,${user.pfp}`} alt='' className='requests-item-pfp' />
            <div className='requests-item-details-container'>
              <h2 className='requests-item-username'>{user.username}</h2>
              <h2 className='requests-item-bio'>{user.bio}</h2>
            </div>
            <div className={isOpen ? 'requests-item-choice-open' : 'requests-item-choice-close'}>
              <img src={Accept} alt="" className="requests-item-icon" onClick={() => acceptRequest(user.id, userId)} />
              <img src={Remove} alt="" className="requests-item-icon" onClick={() => rejectRequest(user.id, userId)} />
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}

export default FriendRequests;
