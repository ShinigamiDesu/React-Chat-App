import React from 'react'
import Phantom from '../../assets/phantom.jpg';
import Logo from '../../assets/logo.png';
import Arrow from '../../assets/arrow.png';
import './groupChats.css'

function groupChats({isOpen}) {
    const testUsers =[
        {
          pfp: Phantom,
          username: "The Antagonists",
          members: "3"
        },
        {
            pfp: Logo,
            username: "The Strawhat Pirates",
            members: "10"
          }
      ];


  return (
    <div className='groupChat-container'>
      <h1 className='group-title'>Your Group Chats:</h1>
      <div className="group-separator"></div>
      <div className='group-container'>
          {
            testUsers.map((user) => (
                <div className='group-item'>
                  <img src={user.pfp} alt='' className='group-item-pfp'/>
                  <div className='group-item-details-container'>
                    <h2 className='group-item-username'>{user.username} <p className="group-item-members"> • {user.members} Members</p> </h2>
                  </div>
                  <button className={isOpen ? 'group-item-btn-open' :  'group-item-btn-close'}>
                    <p className="group-item-btnText">Chat</p>
                    <img src={Arrow} alt="" className="group-item-btn-icon"/>
                  </button>
                </div>
            ))
          }

      </div>
    </div>
  )
}

export default groupChats