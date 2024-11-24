import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import PropTypes from 'prop-types';
import { useAuth } from '../AuthContext';

const Profile = () => {
    const [profile, setProfile] = useState(null);
    const navigate = useNavigate();
    const { logout } = useAuth();

    useEffect(() => {
        const storedProfile = localStorage.getItem('userProfile');
        if (!storedProfile) {
            navigate('/login');
            return;
        }
        setProfile(JSON.parse(storedProfile));
    }, [navigate]);

    const handleLogout = () => {
        try {
            logout();
            navigate('/');
        } catch {
            alert('Помилка виходу з системи');
        }
    };

    if (!profile) return <p>Завантаження...</p>;

    const { fullName, userName, email, phone } = profile;

    return (
        <div className="p-4">
            <div className="max-w-md mx-auto bg-white rounded-lg shadow-lg">
                <div className="p-6">
                    <h2 className="text-2xl font-bold mb-6">{fullName}` профіль</h2>

                    <div className="space-y-4">
                        <InfoItem label="Повне ім'я" value={fullName} />
                        <InfoItem label="Користувач" value={userName} />
                        <InfoItem label="Email" value={email} />
                        <InfoItem label="Телефон" value={phone} />
                    </div>

                    <button
                        onClick={handleLogout}
                        className="w-full mt-6 px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-600"
                    >
                        Вийти
                    </button>
                </div>
            </div>
        </div>
    );
};

const InfoItem = ({ label, value }) => (
    <div className="flex flex-col sm:flex-row sm:justify-between">
        <span className="font-medium text-gray-600">{label}:</span>
        <span className="text-gray-800">{value}</span>
    </div>
);

InfoItem.propTypes = {
    label: PropTypes.string.isRequired,
    value: PropTypes.string.isRequired
};

export default Profile;