import React from 'react';
import './App.css';
import Sidebar from './components/sidebar';
import Home from './pages/Home/home';
import Friends from './pages/Friends/friends';
import Login from './pages/Login/login';
import FriendRequests from './pages/FriendRequests/friendsRequests';
import Search from './pages/SearchUser/searchUser';
import SignUp from './pages/SignUp/SignUp';
import Chat from './pages/Chat/Chat';
import { BrowserRouter, Route, Routes, Navigate } from 'react-router-dom';
import PrivateRoute from './components/PrivateRoute'; // Import the PrivateRoute component

function App() {
    return (
        <BrowserRouter>
            <Routes>
                {/* Redirect the root path to the login page */}
                <Route path="/" element={<Navigate to="/login" />} />

                {/* Public routes */}
                <Route path="/login" element={<Login />} />
                <Route path="/signup" element={<SignUp />} />

                {/* Private routes */}
                <Route element={<PrivateRoute />}>
                    <Route path="/home" element={<Sidebar><Home /></Sidebar>} />
                    <Route path="/friends" element={<Sidebar><Friends /></Sidebar>} />
                    <Route path="/friend-requests" element={<Sidebar><FriendRequests /></Sidebar>} />
                    <Route path="/search-users" element={<Sidebar><Search /></Sidebar>} />
                    <Route path="/user-chat" element={<Sidebar><Chat /></Sidebar>} />
                </Route>
            </Routes>
        </BrowserRouter>
    );
}

export default App;
