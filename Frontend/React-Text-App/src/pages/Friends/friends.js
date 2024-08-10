import React from 'react'
import Gojo from '../../assets/gojo.jpg';
import Sukuna from '../../assets/sukuna.jpg';
import Luffy from '../../assets/luffy.jpeg';
import Remove from '../../assets/remove.png';
import './friends.css'

function friends({isOpen}) {
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
      status: "Offline"
    },
    {
      pfp: Luffy,
      username: "Monkey D. Luffy",
      bio: "I am going to be the King of The Pirates, HAHAHAHAHA!",
      status: "Online"
    }
  ];



  return (
    <div className="friends-container-main">
        <h1 className='friends-title'>Your Friends (3):</h1>
        <div className="friend-separator"></div>
        <div className='friends-container'>
          {
            testUsers.map((user) => (
                <div className='friend-item'>
                  <img src={user.pfp} alt='' className='friend-item-pfp'/>
                  <div className='friend-item-details-container'>
                    <h2 className='friend-item-username'>{user.username} <p className="friend-status"> • {user.status}</p></h2>
                    <h2 className='friend-item-bio'>{user.bio}</h2>
                  </div>
                  <button className={isOpen ? 'friend-item-btn-open' :  'friend-item-btn-close'}>
                    <img src={Remove} alt="" className="friend-item-btn-icon"/>
                    <p className="friend-btnText">Remove</p>
                  </button>
                </div>
            ))
          }

      </div>
    </div>
  )
}

export default friends