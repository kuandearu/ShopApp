import React from 'react';
import { Outlet } from 'react-router-dom';
import SidebarNav from '../AdminPages/Dashboard/SideBarNav';

const Layout: React.FC = () => {
  return (
    <div className="flex">
      <aside className="w-64 fixed h-screen bg-gray-800 text-white p-4">
        <SidebarNav />
      </aside>

      <main className="ml-64 flex-1 p-6">
        <Outlet />
      </main>
    </div>
  );
};

export default Layout;
