import { FC, useState } from "react";
import { CreateListModal } from "../components/CreateListModal";

export const Home: FC = () => {
  const [showModal, setShowModal] = useState(false);

  const toggleModal = () => {
    setShowModal(showModal => !showModal);
  };


  return (
    <div> 
      <button onClick={toggleModal} >Create list</button>
      <CreateListModal showModal={showModal} toggleModal={toggleModal}/>
    </div>
  )
}