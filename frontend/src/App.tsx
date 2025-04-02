import './App.css'
import { CartProvider } from './context/CartContext';
import AdminProjectsPage from './pages/AdminProjectPage';
import CartPage from './pages/CartPage';
import DonatePage from './pages/DonatePage';
import ProjectsPage from './pages/projectsPage';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';



function App() {

  return (
    <>
      <CartProvider>
          <Router>
            <Routes>
              <Route path="/" element={<ProjectsPage/>} />
              <Route path="/donate/:projectName/:projectId" element={<DonatePage/>} />
              <Route path='/cart' element={<CartPage/>} />
              <Route path='/adminProjects' element={<AdminProjectsPage/>}/>
            </Routes>
          </Router>
      </CartProvider>
    </>
  );
}

export default App;
