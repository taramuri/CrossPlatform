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
                console.error('������� ��� ����������� �����:', error);
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
                console.error('������������ ������ ������:', response.data);
                throw new Error('������������ ������ ������');
            }
        } catch (error) {
            console.error('������� �� ��� �������� �����:', error);
            alert('�������: ' + (error.response?.data?.Error || error.message));
        }
    };

    if (!labData) {
        return <p>������������...</p>;
    }

    return (
        <div className="container mt-4">
            <div className="card shadow-sm">
                <div className="card-header">
                    <h2>����������� ������ �{labData.labNumber}</h2>
                </div>

                <div className="card-body">
                    <section>
                        <h5>��������:</h5>
                        <p>{labData.description}</p>
                    </section>

                    <section>
                        <h5>����� ���:</h5>
                        <p>{labData.inputDescription}</p>

                        <h5>������ ���:</h5>
                        <p>{labData.outputDescription}</p>
                    </section>

                    <section className="mt-4">
                        <h5>����`������:</h5>
                        <form onSubmit={handleFormSubmit}>
                            <div className="form-group mb-3">
                                <label htmlFor="inputContent" className="form-label">����� ���:</label>
                                <textarea
                                    className="form-control"
                                    id="inputContent"
                                    rows="4"
                                    value={inputContent}
                                    onChange={handleInputChange}
                                    placeholder="������ ����� ���..."
                                    required
                                />
                            </div>

                            <div className="form-group mb-3">
                                <label htmlFor="outputContent" className="form-label">���������:</label>
                                <textarea
                                    className="form-control"
                                    id="outputContent"
                                    rows="4"
                                    value={outputContent}
                                    readOnly
                                    placeholder="��� �'������� ���������..."
                                />
                            </div>

                            <button type="submit" className="btn btn-outline-primary">����`����</button>
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