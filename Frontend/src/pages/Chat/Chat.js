import React, { useEffect, useState } from 'react';
import Send from '../../assets/send.png';
import './Chat.css';

function Chat({isOpen}) {
  const userId = localStorage.getItem('userId');
  const friendId = localStorage.getItem('friendId');
  const username = localStorage.getItem('ChatUsername');
  const Chatpfp = localStorage.getItem('Chatpfp');
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
    <div className={isOpen ? 'userChat-main-container' : 'userChat-main-close'}>
      <div className='userChat-Top'>
        <img src={`data:image/png;base64,${Chatpfp}`} alt='' className='userChat-img' />
        <h1 className='userChat-username'> • {username}</h1>
      </div>
      <div className="userChat-separator"></div>
      <div className='userChat-Middle'>
        {messages.map((message) => (
          message.senderID.toString() === friendId.toString() ? (
            <div className='userChatFrom' key={message.id}>
              <img src={`data:image/png;base64,${Chatpfp}`} alt='' className='userChat-fromIMG' />
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
        <input className={isOpen ? "userChat-input-open" : "userChat-input-close"} placeholder='type your message here . . .' />
        <img src={Send} alt='' className='userChat-sendIMG' />
      </div>
    </div>
  );
}

export default Chat;
