import { useEffect, useState } from 'react';
import PropTypes from 'prop-types';
import axios from 'axios';

const LabPage = ({ labNumber }) => {
    const [labData, setLabData] = useState(null);
    const [inputContent, setInputContent] = useState('');
    const [outputContent, setOutputContent] = useState('');

    useEffect(() => {
        const fetchLabData = async () => {
            try {
                const response = await axios.get(`http://localhost:5028/api/labs/lab${labNumber}`);
                setLabData(response.data);
            } catch (error) {
                console.error('Помилка при завантаженні даних:', error);
            }
        };

        setInputContent('');
        setOutputContent('');
        fetchLabData();
    }, [labNumber]);

    const handleInputChange = (e) => {
        setInputContent(e.target.value);
    };

    const handleFormSubmit = async (e) => {
        e.preventDefault();

        try {
            const response = await axios.post('http://localhost:5028/api/Labs/run', {
                lab: `lab${labNumber}`,
                inputData: inputContent
            });

            if (response.data?.result) {
                setOutputContent(response.data.result);
            } else {
                console.error('Неочікуваний формат відповіді:', response.data);
                throw new Error('Неочікуваний формат відповіді');
            }
        } catch (error) {
            console.error('Помилка під час відправки форми:', error);
            alert('Помилка: ' + (error.response?.data?.Error || error.message));
        }
    };

    if (!labData) {
        return <p>Завантаження...</p>;
    }

    return (
        <div className="container mt-4">
            <div className="card shadow-sm">
                <div className="card-header">
                    <h2>Лабораторна робота №{labData.labNumber}</h2>
                </div>

                <div className="card-body">
                    <section>
                        <h5>Завдання:</h5>
                        <p>{labData.description}</p>
                    </section>

                    <section>
                        <h5>Вхідні дані:</h5>
                        <p>{labData.inputDescription}</p>

                        <h5>Вихідні дані:</h5>
                        <p>{labData.outputDescription}</p>
                    </section>

                    <section className="mt-4">
                        <h5>Розв`язання:</h5>
                        <form onSubmit={handleFormSubmit}>
                            <div className="form-group mb-3">
                                <label htmlFor="inputContent" className="form-label">Вхідні дані:</label>
                                <textarea
                                    className="form-control"
                                    id="inputContent"
                                    rows="4"
                                    value={inputContent}
                                    onChange={handleInputChange}
                                    placeholder="Введіть вхідні дані..."
                                    required
                                />
                            </div>

                            <div className="form-group mb-3">
                                <label htmlFor="outputContent" className="form-label">Результат:</label>
                                <textarea
                                    className="form-control"
                                    id="outputContent"
                                    rows="4"
                                    value={outputContent}
                                    readOnly
                                    placeholder="Тут з'явиться результат..."
                                />
                            </div>

                            <button type="submit" className="btn btn-outline-primary">Розв`зати</button>
                        </form>
                    </section>
                </div>
            </div>
        </div>
    );
};

LabPage.propTypes = {
    labNumber: PropTypes.string.isRequired,
};

export default LabPage;