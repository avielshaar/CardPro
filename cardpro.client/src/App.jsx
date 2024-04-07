import { BrowserRouter, Route, Routes, Navigate } from 'react-router-dom';
import './App.css';
import LoginPage from './pages/loginPage/LoginPage.jsx';
import SearchPage from './pages/searchPage/SearchPage.jsx';
import IncreasePage from './pages/increasePage/IncreasePage.jsx';

function App() {
  return (
    <BrowserRouter>
      <main>
        <Routes>
          <Route path='/login' element={<LoginPage />}></Route>
          <Route path='/search' element={<SearchPage />}></Route>
          <Route path='/increase/:cardNumber' element={<IncreasePage />} />
          <Route path='*' element={<Navigate to='/login' />} />
        </Routes>
      </main>
    </BrowserRouter>
  );
}

export default App;
