import { Helmet } from 'react-helmet';

const Head = () => {
    return (
        <Helmet>
            <meta charSet="UTF-8" />
            <title>Практичні роботи | Ласкаво просимо!</title>

            <meta name="description"
                content="Онлайн-платформа для виконання лабораторних робіт 1, 2 і 3. Авторизуйтесь, щоб отримати доступ до всіх лабораторних робіт"
            />

            <meta name="viewport"
                content="width=device-width, initial-scale=1, shrink-to-fit=no"
            />
        </Helmet>
    );
};

export default Head;