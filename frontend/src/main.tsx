import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import './index.css';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';

// Import your page components
import ProductList from './AdminPages/Product/ProductList.tsx';
import AddProduct from './AdminPages/Product/AddProduct.tsx';
import UpdateProduct from './AdminPages/Product/UpdateProduct.tsx';
import ProductDetail from './AdminPages/Product/ProductDetail.tsx';
import BrandList from './AdminPages/Brand/BrandList.tsx';
import AddBrand from './AdminPages/Brand/AddBrand.tsx';
import UpdateBrand from './AdminPages/Brand/UpdateBrand.tsx';
import CategoryList from './AdminPages/Category/CategoryList.tsx';
import AddCategory from './AdminPages/Category/AddCategory.tsx';
import UpdateCategory from './AdminPages/Category/UpdateCategory.tsx';
import OrderList from './AdminPages/Order/OrderList.tsx';
import AddOrder from './AdminPages/Order/AddOrder.tsx';
import UpdateOrder from './AdminPages/Order/UpdateOrder.tsx';
import OrderDetail from './AdminPages/Order/OrderDetail.tsx';
import RegisteredUserList from './AdminPages/RegisteredUser/RegisteredUserList.tsx';
import AddRegisteredUser from './AdminPages/RegisteredUser/AddRegisteredUser.tsx';
import UpdateRegisteredUser from './AdminPages/RegisteredUser/UpdateRegisteredUser.tsx';
import RegisteredUserDetail from './AdminPages/RegisteredUser/RegisteredUserDetail.tsx';
import AdminList from './AdminPages/Admin/AdminList.tsx';
import AddAdmin from './AdminPages/Admin/AddAdmin.tsx';
import UpdateAdmin from './AdminPages/Admin/UpdateAdmin.tsx';
import AdminDetail from './AdminPages/Admin/AdminDetail.tsx';
import AddProductToOrder from './AdminPages/Order/AddProductToOrder.tsx';
import Layout from './Components/Layout.tsx';

// Import your custom Layout component instead of using lucide-react's Layout

const router = createBrowserRouter([
  {
    path: "/", // Top-level route with a custom layout
    element: <Layout />,
    children: [
      { path: "product/all", element: <ProductList /> },
      { path: "add-product", element: <AddProduct /> },
      { path: "update-product/:id", element: <UpdateProduct /> },
      { path: "product/:id", element: <ProductDetail /> },
      { path: "brand/all", element: <BrandList /> },
      { path: "add-brand", element: <AddBrand /> },
      { path: "update-brand/:id", element: <UpdateBrand /> },
      { path: "category/all", element: <CategoryList /> },
      { path: "add-category", element: <AddCategory /> },
      { path: "update-category/:id", element: <UpdateCategory /> },
      { path: "order/all", element: <OrderList /> },
      { path: "add-order", element: <AddOrder /> },
      { path: "update-order/:id", element: <UpdateOrder /> },
      { path: "order/:id", element: <OrderDetail /> },
      { path: "order/add-product/:id", element: <AddProductToOrder /> },
      { path: "registerUser/all", element: <RegisteredUserList /> },
      { path: "add-registerUser", element: <AddRegisteredUser /> },
      { path: "update-registerUser/:id", element: <UpdateRegisteredUser /> },
      { path: "registerUser/:id", element: <RegisteredUserDetail /> },
      { path: "admin/all", element: <AdminList /> },
      { path: "add-admin", element: <AddAdmin /> },
      { path: "update-admin/:id", element: <UpdateAdmin /> },
      { path: "admin/:id", element: <AdminDetail /> },
    ],
  },
]);

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <RouterProvider router={router} />
  </StrictMode>
);
