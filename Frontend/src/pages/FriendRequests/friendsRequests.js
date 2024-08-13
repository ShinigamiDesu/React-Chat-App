import React from 'react'
import './friendsRequests.css'
import Goko from '../../assets/goku.png';
import Yhwach from '../../assets/yhwach.png';
import Accept from '../../assets/accept.png';
import Remove from '../../assets/reject.png';


function FriendRequests({isOpen}) {
  const testUsers =[
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

  const handleClick = (type) =>{
    if (type === "accept") {
      alert('You clicked ' + type);
    } else {
      alert('You clicked ' + type);
    }
  }



  return (
    <div className="requests-container-main">
        <h1 className='requests-title'>Friend Requests (2):</h1>
        <div className="requests-separator"></div>
        <div className='requests-container'>
          {
            testUsers.map((user) => (
                <div className='requests-item'>
                  <img src={user.pfp} alt='' className='requests-item-pfp'/>
                  <div className='requests-item-details-container'>
                    <h2 className='requests-item-username'>{user.username}</h2>
                    <h2 className='requests-item-bio'>{user.bio}</h2>
                  </div>
                  <div className={isOpen ? 'requests-item-choice-open' :  'requests-item-choice-close'}>
                    <img src={Accept} alt="" className="requests-item-icon" onClick={() => handleClick("accept")}/>
                    <img src={Remove} alt="" className="requests-item-icon" onClick={() => handleClick("Reject")}/>
                  </div>
                </div>
            ))
          }

      </div>
    </div>
  )
}

export default FriendRequests