import React, { useState } from 'react';
import { Link } from 'react-router-dom';

const SidebarNav: React.FC = () => {
  const [isDashboardOpen, setDashboardOpen] = useState(false);

  return (
    <nav className="flex flex-col space-y-2">
      <button
        onClick={() => setDashboardOpen(!isDashboardOpen)}
        className="text-white hover:bg-gray-700 p-2 rounded flex justify-between items-center"
      >
        🧭 Dashboard
        <span>{isDashboardOpen ? '▲' : '▼'}</span>
      </button>

      {/* Dropdown Items */}
      {isDashboardOpen && (
        <div className="ml-4 flex flex-col space-y-1 transition-all duration-300">
          <Link to="/product/all" className="text-white hover:bg-gray-700 p-2 rounded">📦 Products</Link>
          <Link to="/brand/all" className="text-white hover:bg-gray-700 p-2 rounded">🏷️ Brands</Link>
          <Link to="/category/all" className="text-white hover:bg-gray-700 p-2 rounded">🗂️ Categories</Link>
          <Link to="/order/all" className="text-white hover:bg-gray-700 p-2 rounded">📋 Orders</Link>
          <Link to="/registerUser/all" className="text-white hover:bg-gray-700 p-2 rounded">👥 Registered Users</Link>
          <Link to="/admin/all" className="text-white hover:bg-gray-700 p-2 rounded">🛠️ Admins</Link>
        </div>
      )}
    </nav>
  );
};

export default SidebarNav;
