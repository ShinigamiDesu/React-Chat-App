import React from 'react'
import Gojo from '../../assets/gojo.jpg';
import Send from '../../assets/send.png';
import './Chat.css'

function Chat() {
  return (
    <div className='userChat-main-container'>
        <div className='userChat-Top'>
          <img src={Gojo} alt='' className='userChat-img'/>
          <h1 className='userChat-username'> • Saturo Gojo</h1>
        </div>
        <div className="userChat-separator"></div>
        <div className='userChat-Middle'>
          <div className='userChatFrom'>
            <img src={Gojo} alt='' className='userChat-fromIMG'/>
            <p className='userChat-fromMSG'>HI IM SATURO GOJOOOOOO</p>
          </div>
          <div className='userChatTo'>
            <p className='userChat-ToMSG'>Hi Gojo, Im Ali, ready to fight?</p>
          </div>
        </div>
        <div className='userChat-Bottom'>
          <input className="userChat-input" placeholder='type your message here . . .'/>
          <img src={Send} alt='' className='userChat-sendIMG'/>
        </div>
    </div>
  )
}

export default Chat