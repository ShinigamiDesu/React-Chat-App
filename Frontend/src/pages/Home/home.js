import React from 'react';
import Gojo from '../../assets/gojo.jpg';
import Sukuna from '../../assets/sukuna.jpg';
import Luffy from '../../assets/luffy.jpeg';
import Goko from '../../assets/goku.png';
import Yhwach from '../../assets/yhwach.png';
import Chat from '../../assets/chat.png';
import './home.css';

function Home({isOpen}) {
  const testUsers =[
    {
      pfp: Gojo,
      username: "Gojo Saturo",
      bio: "The Strongest Sorcerer You Know :D!",
      status: "Online"
    },
    {
      pfp: Sukuna,
      username: "Ryomen Sukuna",
      bio: "The Strongest Sorcerer of All Time, Also the King of Curses!",
      status: "Online"
    },
    {
      pfp: Luffy,
      username: "Monkey D. Luffy",
      bio: "I am going to be the King of The Pirates, HAHAHAHAHA!",
      status: "Online"
    },
    {
      pfp: Goko,
      username: "Son Goku",
      bio: "Hi i am Goku, I heard you are strong. Lets Spar!",
      status: "Offline"
    },
    {
      pfp: Yhwach,
      username: "Yhwach",
      bio: "The one who will rob you of everything...",
      status: "Offline"
    }
  ];


  return (
    <div className='home-container'>
      <h1 className='home-title'>Your Recent Chats:</h1>
      <div className="separator"></div>
      <div className='chat-container'>
          {
            testUsers.map((user) => (
                <div className='chat-item'>
                  <img src={user.pfp} alt='' className='item-pfp'/>
                  <div className='item-details-container'>
                    <h2 className='item-username'>{user.username} <p className="item-status"> • {user.status}</p> </h2>
                    <h2 className='item-bio'>{user.bio}</h2>
                  </div>
                  <button className={isOpen ? 'item-btn-open' :  'item-btn-close'}>
                    <img src={Chat} alt="" className="item-btn-icon"/>
                    <p className="item-btnText">Chat</p>
                  </button>
                </div>
            ))
          }

      </div>
    </div>
  );
}

export default Home;
