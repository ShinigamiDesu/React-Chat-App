import React, { useState } from 'react';
import './App.css';
import Sidebar from './components/sidebar';
import Home from './pages/Home/home';
import Friends from './pages/Friends/friends';
import Login from './pages/Login/login';
import FriendRequests from './pages/FriendRequests/friendsRequests';
import GroupChats from './pages/GroupChats/groupChats';
import Search from './pages/SearchUser/searchUser';
import SignUp from './pages/SignUp/SignUp';
import { BrowserRouter, Route, Routes } from 'react-router-dom';

function App() {
    return (
        <BrowserRouter>
            <Routes>
                <Route
                    path="/"
                    element={<Sidebar><Home /></Sidebar>}
                />
                <Route
                    path="/friends"
                    element={<Sidebar><Friends /></Sidebar>}
                />
                <Route
                    path="/friend-requests"
                    element={<Sidebar><FriendRequests /></Sidebar>}
                />
                <Route
                    path="/group-chats"
                    element={<Sidebar><GroupChats /></Sidebar>}
                />
                <Route
                    path="/search-users"
                    element={<Sidebar><Search /></Sidebar>}
                />
                <Route path="/login" element={<Login />} />
                <Route path="/signup" element={<SignUp />} />
            </Routes>
        </BrowserRouter>
    );
}

export default App;