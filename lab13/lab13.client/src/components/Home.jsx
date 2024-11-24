import { useNavigate } from 'react-router-dom';

const Home = () => {
    const navigate = useNavigate();

    const handleLoginClick = () => {
        navigate('/login');
    };

    return (
        <div className="container text-center py-5">
            <h1 className="mb-4">������� �������!</h1>
            <p className="mb-4">
                ��� ���������� �������� ���������� �������� ������ 1, 2 �� 3.
                ���� �����, ������ ��� �������������, ��� ��������� ������.
            </p>
            <button
                className="btn btn-primary mt-3"
                onClick={handleLoginClick}
            >
                ����� �����
            </button>
        </div>
    );
};

export default Home;