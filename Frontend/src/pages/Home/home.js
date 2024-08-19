import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import Chat from '../../assets/chat.png';
import './home.css';

function Home({isOpen}) {
  const navigate = useNavigate();
  const userId = localStorage.getItem("userId");
  const [chats, setChats] = useState([]);
  const [hasChats, setHasChats] = useState(true);

  useEffect(() => {
    const fetchChats = async () => {
      try{
        const response = await fetch(`https://localhost:7245/api/UserChat/GetChats/${userId}`, {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json'
          }
        });

        if(response.ok){
          const data = await response.json();
          console.log('fetched chats');
          setChats(data);
        }
        else if(response.status === 404){
          setHasChats(false);
        }
        else {
          console.error('Failed to fetch chats');
        }
      }
      catch(error){
        console.log('Catch Error: ' + error);
      }
    }
    
    fetchChats();
  }, [userId]);

    const navigateToChat = (friendId, username, pfp) => {
      localStorage.setItem('friendId', friendId);
      localStorage.setItem('ChatUsername', username);
      localStorage.setItem('Chatpfp', pfp);
      navigate('/user-chat');
    }

  return (
    <div className='home-container'>
      <h1 className='home-title'>{hasChats ? `Your Recent Chats (${chats.length}):`  : 'You Have No Recent Chats'}</h1>
      <div className="separator"></div>
      {hasChats && (
          <div className='chat-container'>
          {
            chats.map((chat) => (
                <div className='chat-item'>
                  <img src={`data:image/png;base64,${chat.pfp}`} alt='' className='item-pfp' key={chat.id}/>
                  <div className='item-details-container'>
                    <h2 className='item-username'>{chat.username} <p className="item-status"> • {chat.status}</p> </h2>
                    <h2 className='item-bio'>{chat.bio}</h2>
                  </div>
                  <button className={isOpen ? 'item-btn-open' :  'item-btn-close'}  onClick={() => navigateToChat(chat.id, chat.username, chat.pfp)}>
                    <img src={Chat} alt="" className="item-btn-icon"/>
                    <p className="item-btnText">Chat</p>
                  </button>
                </div>
            ))
          }
      </div>
        )}
    </div>
  );
}

export default Home;
