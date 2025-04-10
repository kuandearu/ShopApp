interface Product {
    id: number;
  name: string;
  price: number;
  oldPrice:number;
  image: string;
  description: string;
  specification:string;
  buyTurn: number;
  quantity:number;
  brandId: number;
  categoryId:number;
  createdAt: Date;
  updatedAt: Date;
};

interface Brand {
  id: number; 
  name: string;
  image: string;
};

interface Category {
  id: number; 
  name: string;
  image: string;
};

interface Order {
    id : number;
    userId: number;
    status: number;
    note: string;
    total: number;
    createdAt: Date,
    updatedAt: Date,
    orderDetails: OrderDetail[],
}
interface OrderDetail {
  id: number;
  orderId: number;
  productId: number;
  price: number;
  quantity: number;
  createdAt: Date;
  updatedAt: Date;
}

interface Admin {
  id : number;
  email : string;
  passwordHash  : string;
  name : string;
  role : number;
  avatar : string;
  phone : number;
  updatedAt : Date;
  createdAt : Date;

}

interface RegisteredUser {
  id : number;
  email : string;
  passwordHash  : string;
  name : string;
  role : number;
  avatar : string;
  phone : number;
  updatedAt : Date;
  createdAt : Date;
}

interface User {
  id : number;
  email : string;
  passwordHash  : string;
  name : string;
  role : number;
  avatar : string;
  phone : number;
  updatedAt : Date;
  createdAt : Date;

}