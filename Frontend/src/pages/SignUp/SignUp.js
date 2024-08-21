import React, { useState } from 'react';
import './signUp.css';
import img from '../../assets/img.png';
import { useNavigate } from 'react-router-dom';

function SignUp() {
  const [image, setImage] = useState(null);
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [bio, setBio] = useState('');
  const navigate = useNavigate();

  const handleLogin = () => {
    navigate('/login');
  };

  const handleImageChange = (event) => {
    setImage(event.target.files[0]);
  };

  const handleSignUp = async () => {
    if (!username || !password || !bio || !image) {
      alert("Please fill in all fields.");
      return;
    }

    const formData = new FormData();
    formData.append('PFP', image);
    formData.append('Username', username);
    formData.append('Password', password);
    formData.append('Bio', bio);  // Include the bio field in the form data

    try {
      const response = await fetch(`${process.env.REACT_APP_API_URL}/api/User/Registration`, {
        method: 'POST',
        body: formData,
      });
  
      console.log('Response:', response); // Log the response
      const data = await response.text(); // Use text() instead of json() to capture any possible response content
  
      console.log('Data:', data); // Log the data
  
      if (response.ok) {
        alert('Registration successful!');
        navigate('/login');
      } else {
        alert(`Registration failed: ${data}`);
      }
    } catch (error) {
      console.error('Error:', error);
      alert('Registration failed: Unable to connect to the server.');
    }
  };

  return (
    <div>
      <div className="title-container">
        <h1 className="welcome">Welcome To</h1>
        <h1 className="mugiwara">MiguwaraChat</h1>
      </div>

      <div className="signup-container">
        <div className="signup-form">
          <h2 className="signup-title">Sign Up!</h2>
          {image ? (
            <img src={URL.createObjectURL(image)} alt="User Selected" className="IMG" />
          ) : (
            <img src={img} alt="Signup Illustration" className="IMG" />
          )}
          <input
            className="pfp-input"
            type="file"
            onChange={handleImageChange}
          />
          <h2 className="signup-username-title">Username</h2>
          <input
            className="signup-username"
            type="text"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
          />
          <h2 className="signup-password-title">Password</h2>
          <input
            className="signup-password"
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
          <h2 className="signup-username-title">Bio</h2>
          <input
            className="signup-username"
            type="text"
            value={bio}
            onChange={(e) => setBio(e.target.value)}
          />
          <button className="signup-btn" onClick={handleSignUp}>Sign Up</button>
          <div className="signup-separator"></div>
          <div className="sign-container">
            <p className="login1">Already Have An Account?</p>
            <p id="login" className="login2" onClick={handleLogin}>Login Now!</p>
          </div>
        </div>
      </div>
    </div>
  );
}

export default SignUp;
