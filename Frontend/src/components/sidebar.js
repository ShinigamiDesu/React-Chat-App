import React, { useState, cloneElement } from 'react';
import Bars from '../assets/bars.png';
import HomeIcon from '../assets/home.png';
import LogoutIcon from '../assets/logout.png';
import FriendsIcon from '../assets/friends.png';
import FriendRequest from '../assets/heart.png';
import GroupChats from '../assets/group.png';
import Search from '../assets/search.png';
import { NavLink } from 'react-router-dom';
import { useNavigate } from 'react-router-dom';

function Sidebar({ children }) {
    const navigate = useNavigate();
    const [isOpen, setIsOpen] = useState(true);
    const toggle = () => setIsOpen(!isOpen);
    const [currMenu, setCurrMenu] = useState("");

    const handleLogout = () => {
        localStorage.removeItem('userId');
        navigate('/login');
    };

    const menuItem = [
        {
            path: "/home",
            name: "Home",
            icon: HomeIcon
        },
        {
            path: "/friends",
            name: "Friends",
            icon: FriendsIcon
        },
        {
            path: "/friend-requests",
            name: "Friend Requests",
            icon: FriendRequest
        },
        {
            path: "/group-chats",
            name: "Group Chats",
            icon: GroupChats
        },
        {
            path: "/search-users",
            name: "Search User",
            icon: Search
        },
    ];

    const logoutItem = {
        path: "/login",
        name: "Logout",
        icon: LogoutIcon,
        onClick: handleLogout
    };

    const renderChildren = () => cloneElement(children, { isOpen });

    return (
        <div className='sidebar-container'>
            <div className={isOpen ? "open-sidebar" : "close-sidebar"}>
                <div className='top-section'>
                    <h1 className={isOpen ? "logo" : "none"}>MugiwaraChat</h1>
                    <img src={Bars} alt="Menu Bars" className={isOpen ? "open-bars" : "close-bars"} onClick={toggle}/>
                </div>
                {
                    menuItem.map((item, index) => (
                        <NavLink to={item.path} key={index} className={currMenu === item.name ? "active-item-link" : "item-link"} onClick={()=>setCurrMenu(item.name)}>
                            <div>
                                <img src={item.icon} alt="" className='item-icon'/>
                            </div>
                            <div className={isOpen ? "item-text-open" : "item-text-close"}>{item.name}</div>
                        </NavLink>
                    ))
                }
                <div className={isOpen ? "logout-item-open" : "logout-item-close"}>
                    <NavLink to={logoutItem.path} className="item-link">
                        <div className='item-icon'>
                            <img src={logoutItem.icon} alt='' className='item-icon'/>
                        </div>
                        <div className={isOpen ? "item-text-open" : "item-text-close"}>{logoutItem.name}</div>
                    </NavLink>
                </div>
            </div>
            <main>
                {renderChildren()}
            </main>
        </div>
    );
}

export default Sidebar;