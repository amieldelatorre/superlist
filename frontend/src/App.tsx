// import { useState } from 'react'
import { Route, Routes } from "react-router-dom"
import { Home } from "./pages/Home"
import { NotFound } from "./pages/NotFound"

function App() {
  fetch("http://localhost:8080/health")
  .then(res => res.text())
  .then(console.log)
  
  return (
    <div className="main-wrapper">    
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="*" element={<NotFound />} />
      </Routes>
    </div>
  )
}

export default App
