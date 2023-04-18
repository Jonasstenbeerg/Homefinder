import './App.css';
import Navbar from "./components/navbar/Navbar"
import Home from "./components/home/Home";
import Login from "./components/authentication/Login";
import {Routes, Route } from "react-router-dom";

function App() {
  return (
    <>
      <Navbar />
      <main>
        <Routes>
          <Route path='/' element={<Home />}/>
          <Route path='/Login' element={<Login />}/>
        </Routes>
      </main>
    </>
  );
}

export default App;
