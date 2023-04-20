import './App.css';
import Navbar from "./components/navbar/Navbar"
import Home from "./components/home/Home";
import ForgotPassword from './components/authentication/ForgotPassword';
import Login from "./components/authentication/Login";
import {Routes, Route, Navigate } from "react-router-dom";
import { RequireAuth } from 'react-auth-kit';

const loginPath = '/login'
const homePath = '/'

function App() {
  return (
    <>
      <Navbar />
      <main>
        <Routes>
          <Route path={homePath} element={<RequireAuth loginPath={loginPath}>
            <Home />
          </RequireAuth>}/>
          <Route path={loginPath} element={<Login />}/>
          <Route path='/forgot-password' element={<ForgotPassword />}/>
          <Route path="*" element={<Navigate replace to={homePath} />} />
        </Routes>
      </main>
    </>
  );
}

export default App;
