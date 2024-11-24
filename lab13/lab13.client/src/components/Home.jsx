import { useNavigate } from 'react-router-dom';

const Home = () => {
    const navigate = useNavigate();

    const handleLoginClick = () => {
        navigate('/login');
    };

    return (
        <div className="container text-center py-5">
            <h1 className="mb-4">Ласкаво просимо!</h1>
            <p className="mb-4">
                Цей застосунок дозволяє виконувати практичні роботи 1, 2 та 3.
                Будь ласка, увійдіть або зареєструйтеся, щоб розпочати роботу.
            </p>
            <button
                className="btn btn-primary mt-3"
                onClick={handleLoginClick}
            >
                Увійти зараз
            </button>
        </div>
    );
};

export default Home;