import React from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { jwtDecode } from 'jwt-decode';  // Import correctly

const PrivateRoute = () => {
    const token = localStorage.getItem('token');

    if (token) {
        try {
            const decodedToken = jwtDecode(token); // Use jwtDecode correctly
            const currentTime = Date.now() / 1000;

            if (decodedToken.exp < currentTime) {
                // Token has expired
                localStorage.removeItem('token');
                return <Navigate to="/login" />;
            }

            return <Outlet />;
        } catch (error) {
            // Invalid token
            localStorage.removeItem('token');
            return <Navigate to="/login" />;
        }
    }

    return <Navigate to="/login" />;
};

export default PrivateRoute;
