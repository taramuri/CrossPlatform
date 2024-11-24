import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import { useAuth } from '../AuthContext';

const Login = () => {
    const [formData, setFormData] = useState({
        email: '',
        password: '',
    });
    const [error, setError] = useState('');
    const [isLoading, setIsLoading] = useState(false);
    const navigate = useNavigate();
    const { login } = useAuth();

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData({ ...formData, [name]: value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError('');
        setIsLoading(true);

        try {
            const response = await axios.post('http://localhost:5028/api/Account/login', formData, {
                headers: {
                    'Content-Type': 'application/json',
                },
                validateStatus: false,
                withCredentials: true
            });

            console.log('Response:', response); 

            if (response.status === 200) {
                login(response.data);
                const userProfile = response.data.userProfile || response.data;
                localStorage.setItem('userProfile', JSON.stringify(userProfile));
                navigate('/profile');
            } else {
                setError(`Server responded with status ${response.status}: ${response.data?.message || 'Unknown error'}`);
            }
        } catch (error) {
            console.error('Login error:', error);

            if (error.response) {
                console.log('Error response:', error.response);
                if (error.response.status === 401) {
                    setError('Invalid email or password. Please try again.');
                } else if (error.response.data?.error) {
                    setError(error.response.data.error);
                } else {
                    setError(`Server error: ${error.response.status}`);
                }
            } else if (error.request) {
                setError('Cannot connect to the server. Please check if the API is running.');
            } else {
                setError(`Error: ${error.message}`);
            }
        } finally {
            setIsLoading(false);
        }
    };

    return (
        <form onSubmit={handleSubmit} className="container mt-5">
            <h2>Login</h2>
            {error && (
                <div className="alert alert-danger" role="alert">
                    {error}
                </div>
            )}
            <div className="form-group mb-3">
                <label htmlFor="email">Email</label>
                <input
                    className="form-control"
                    type="email"
                    id="email"
                    name="email"
                    placeholder="Email"
                    value={formData.email}
                    onChange={handleChange}
                    required
                />
            </div>
            <div className="form-group mb-3">
                <label htmlFor="password">Password</label>
                <input
                    className="form-control"
                    type="password"
                    id="password"
                    name="password"
                    placeholder="Password"
                    value={formData.password}
                    onChange={handleChange}
                    required
                />
            </div>
            <button
                className="btn btn-primary"
                type="submit"
                disabled={isLoading}
            >
                {isLoading ? 'Logging in...' : 'Login'}
            </button>
        </form>
    );
};

export default Login;