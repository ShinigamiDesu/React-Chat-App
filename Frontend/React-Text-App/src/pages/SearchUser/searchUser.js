import React from 'react'
import './searchUser.css'
import Search from '../../assets/search.png';
import Sukuna from '../../assets/sukuna.jpg';
import Luffy from '../../assets/luffy.jpeg';
import Chat from '../../assets/chat.png';
import Add from '../../assets/add.png';

function searchUser({isOpen}) {
  const testUsers =[
    {
      pfp: Sukuna,
      username: "Ryomen Sukuna",
      bio: "The Strongest Sorcerer of All Time, Also the King of Curses!",
    },
    {
      pfp: Luffy,
      username: "Monkey D. Luffy",
      bio: "I am going to be the King of The Pirates, HAHAHAHAHA!",
    }
  ];

  return (
    <div className='main-container'>
       <h1 className='home-title'>Search a username:</h1>
       <div className='div-test'>
        <input className='search-input' placeholder='Username'></input>
        <img src={Search} alt='' className='search-icon'/>
       </div>
      <div className="separator"></div>
      <div className='search-container'>
          {
            testUsers.map((user) => (
                <div className='search-item'>
                  <img src={user.pfp} alt='' className='search-pfp'/>
                  <div className='search-details-container'>
                    <h2 className='search-username'>{user.username}</h2>
                    <h2 className='search-bio'>{user.bio}</h2>
                  </div>

                  <div className={isOpen ? 'btn-div-open' :  'btn-div-close'}>
                    <button className='search-btnRQ'>
                      <img src={Add}alt='' className='search-btnRQIcon'/>
                      <p className="search-btnRQText">Send Request</p>
                    </button>
                    <button className='search-btnMSG'>
                      <img src={Chat} alt='' className='search-btnMSGIcon'/>
                      <p className="search-btnMSGText">Send Message</p>
                    </button>
                  </div>
                  
                </div>
            ))
          }

      </div>
    </div>
  )
}

export default searchUser