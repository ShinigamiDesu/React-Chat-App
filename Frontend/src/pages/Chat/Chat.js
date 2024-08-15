import React, { useEffect, useState } from 'react';
import Send from '../../assets/send.png';
import './Chat.css';

function Chat() {
  const userId = localStorage.getItem('userId');
  const friendId = localStorage.getItem('friendId');
  const username = localStorage.getItem('username');
  const pfp = localStorage.getItem('pfp');
  const [messages, setMessages] = useState([]);

  useEffect(() => {
    const fetchMessages = async () => {
      try {
        const response = await fetch(`https://localhost:7245/api/UserChat/GetMessages/${userId}/${friendId}`, {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json',
          },
        });
        if (response.ok) {
          const data = await response.json();
          setMessages(data);
        } else if (response.status === 404) {
          console.log('Failed to fetch messages');
        } else {
          console.log('Something went wrong');
        }
      } catch (error) {
        console.log(error);
      }
    };
    fetchMessages();
  }, [userId, friendId]);

  return (
    <div className='userChat-main-container'>
      <div className='userChat-Top'>
        <img src={`data:image/png;base64,${pfp}`} alt='' className='userChat-img' />
        <h1 className='userChat-username'> • {username}</h1>
      </div>
      <div className="userChat-separator"></div>
      <div className='userChat-Middle'>
        {messages.map((message) => (
          message.senderID.toString() === friendId.toString() ? (
            <div className='userChatFrom' key={message.id}>
              <img src={`data:image/png;base64,${pfp}`} alt='' className='userChat-fromIMG' />
              <p className='userChat-fromMSG'>{message.message}</p>
            </div>
          ) : (
            <div className='userChatTo' key={message.id}>
              <p className='userChat-ToMSG'>{message.message}</p>
            </div>
          )
        ))}
      </div>
      <div className='userChat-Bottom'>
        <input className="userChat-input" placeholder='type your message here . . .' />
        <img src={Send} alt='' className='userChat-sendIMG' />
      </div>
    </div>
  );
}

export default Chat;
