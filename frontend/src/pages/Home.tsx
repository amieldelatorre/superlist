import { FC, useState } from "react";
import { CreateListModal } from "../features/createList/CreateList";
import '../css/create-form.css'

export const Home: FC = () => {
  const [showModal, setShowModal] = useState(false);
  const [showCreateListButton, setShowCreateListButton] = useState(true);

  const createListButtonModalToggle = () => {
    setShowModal(showModal => !showModal);
    setShowCreateListButton(showCreateListButton => !showCreateListButton);
  };

  return (
    <>
      <h1>Title</h1>
      <h1>Title</h1>
      <h1>Title</h1>
      <h1>Title</h1>
      <h1>Title</h1>
      <h1>Title</h1>
      <h1>Title</h1>
      <h1>Title</h1>
      <h1>Title</h1>
      <h1>Title</h1>
      <h1>Title</h1>
      <h1>Title</h1>
      <h1>Title</h1>
      <h1>Title</h1>
      <h1>Title</h1>
      <h1>Title</h1>
      <h1>Title</h1>
      <h1>Title</h1>
      <h1>Title</h1>
      { showCreateListButton && <button className="create-list-button" onClick={createListButtonModalToggle}>Create list</button> }
      <CreateListModal showModal={showModal} toggleShowModal={createListButtonModalToggle}/>
    </>
  )
}