// import { useState } from 'react'
import { Suspense } from 'react';
import { RouterProvider, createBrowserRouter, RouteObject } from "react-router-dom"
import { Home } from "./pages/Home"
import { NotFound } from "./pages/NotFound"
import { ViewList } from "./pages/ViewList"

function App() {
  const routes: RouteObject[] = [
    {
      path: "/vlis/:id",
      element: <ViewList />,
    },
    {
      path: "/",
      element: <Home />
    },
    {
      path: "*",
      element: <NotFound />
    }
  ]

  return (
    <div className="main-wrapper">   
    <Suspense>
      <RouterProvider router={createBrowserRouter(routes)} />
    </Suspense> 
    </div>
  )
}

export default App
