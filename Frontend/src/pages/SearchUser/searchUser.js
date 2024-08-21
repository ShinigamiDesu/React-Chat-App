import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import './searchUser.css';
import Search from '../../assets/search.png';
import Chat from '../../assets/chat.png';
import Add from '../../assets/add.png';

function SearchUser({isOpen}) {
  const navigate = useNavigate();
  const userId = localStorage.getItem("userId");
  const usernameUser = localStorage.getItem("username");
  const [username, setUsername] = useState('');
  const [users, setUsers] = useState([]);
  const [friends, setFriends] = useState([]);

  useEffect(() => {
    const fetchFriends = async () => {
      try {
        const response = await fetch(`https://localhost:7245/api/UserFriends/GetFriends/${userId}`, {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json'
          }
        });

        if (response.ok) {
          const data = await response.json();
          setFriends(data.map(friend => friend.id));
        } else {
          console.log('Failed to fetch friends');
        }
      } catch (error) {
        console.log('Something Went Wrong, Catch');
      }
    };
    fetchFriends();
  }, [userId]);

  const getUsers = async () =>{
    try{
      const response = await fetch(`https://localhost:7245/api/User/GetUsers/${username}`,{
        method: 'GET',
        headers: {
          'Content-Type': 'application/json'
        }
      });
      if(response.ok){
        const data = await response.json();
        setUsers(data);
      }
      else if(response.status === 404){
        console.log('Nothing Found');
        setUsers([]); // Ensure users array is cleared when nothing is found
      }
    }
    catch(error){
      console.log('Something Went Wrong, Catch');
      setUsers([]); // Ensure users array is cleared in case of an error
    }
  };

  const sendFriendRequest = async (friend, friendId) =>{
    try{
      const response = await fetch(`https://localhost:7245/api/UserFriends/AddFriend/${userId}/${friendId}`,{
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        }
      });
      if(response.ok){
        alert('A Friend Request Has Been Sent To: ' + friend)
      }
      else if(response.status === 400){
        alert(' A Friend Request is Already in Place ');
      }
    }
    catch(error){
      console.log('Something Went Wrong, Catch');
    }
  };

  const navigateToChat = (friendId, username, pfp) => {
    localStorage.setItem('friendId', friendId);
    localStorage.setItem('ChatUsername', username);
    localStorage.setItem('Chatpfp', pfp);
    navigate('/user-chat');
  }

  const addRecentChat = async (friendId) =>{
    try{
      const response = await fetch(`${process.env.REACT_APP_API_URL}/api/UserChat/newRecentChat/${userId}/${friendId}`,{
        method: 'POST',
        headers:{
          'Content-Type': 'application/json',
        }
      });

      if(response.ok){
        console.log('Success');
      }
    }
    catch(error){
      console.log('Catch Went Wrong');
    }
  }

  return (
    <div className={isOpen ? 'search-main-container' : 'search-main-close'}>
       <h1 className='home-title'>Search a username:</h1>
       <div className='div-test'>
        <input className='search-input' placeholder='Username' value={username} onChange={(e) => setUsername(e.target.value)}></input>
        <img src={Search} alt='' className='search-icon' onClick={getUsers}/>
       </div>
      <div className="separator"></div>

      {users.length === 0 ? (
        <h1 className='no-users-found'>No Users Found</h1>
      ) : (
        <div className='search-container'>
          {
            users.map((user) => (
              <div className='search-item' key={user.id}>
                <img src={`data:image/png;base64,${user.pfp}`} alt='' className='search-pfp'/>
                <div className='search-details-container'>
                  <h2 className='search-username'>{user.username}</h2>
                  <h2 className='search-bio'>{user.bio}</h2>
                </div>

                {user.username !== usernameUser ? (
                  <div className={isOpen ? 'btn-div-open' :  'btn-div-close'}>
                    {!friends.includes(user.id) && (
                      <button className='search-btnRQ' onClick={() => sendFriendRequest(user.username, user.id)}>
                        <img src={Add} alt='' className='search-btnRQIcon'/>
                        <p className="search-btnRQText">Send Request</p>
                      </button>
                    )}
                    <button className='search-btnMSG' onClick={() => {
                                                     addRecentChat(user.id);
                                                     navigateToChat(user.id, user.username, user.pfp);
                                                    }}>
                      <img src={Chat} alt='' className='search-btnMSGIcon'/>
                      <p className="search-btnMSGText">Send Message</p>
                    </button>
                  </div>
                ) : (
                  <div></div>
                )}
              </div>
            ))
          }
        </div>
      )}
    </div>
  );
}

export default SearchUser;
