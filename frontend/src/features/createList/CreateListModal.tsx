import { FC, ChangeEvent, useState, useEffect, useCallback } from "react";
import { AcceptedCreateListFormData, sendApiRequest } from '../../api/api';
import { CreateListModalContent } from "./CreateListModalContent";

interface CreateListModalProps {
  showModal: boolean;
  toggleShowModal(): void;
}

export type CreateListFormData = {
  createdBy: string;
  title: string;
  description: string;
  passphrase: string;
  expiry: Date;
  errors: { [key: string]: string };
};

export const CreateListModal: FC<CreateListModalProps> = ({showModal, toggleShowModal}: CreateListModalProps) => {
  const [formData, setFormData] = useState<CreateListFormData>({createdBy: "", title: "", description: "", passphrase: "", expiry: new Date(Date.now() + (3600 * 1000 * 24)), errors: {}});
  
  const handleEscKey = useCallback((e: KeyboardEvent) => {
    if (e.key === "Escape") {
      toggleShowModal();
    }
  }, [toggleShowModal]);

  useEffect(() => {
    document.addEventListener('keyup', handleEscKey, false);
  });

  const createListFormSubmit = async (e: React.FormEvent) => {
    e.preventDefault()

    // validateCreateForm();
    let curDate = new Date();
    let minDate = new Date(curDate.getTime() + 30 * 60000); // Minimum 30 minutes
    let maxDate = new Date(curDate.getTime() + 30 * 86400000); // Maximum 30 days
    let errors: { [key: string]: string } = {};

    if (formData.passphrase.length < 8) errors["Passphrase"] = "must be at least 8 characters long";
    if (formData.title.length < 1) errors["Title"] = "cannot be empty";
    if (formData.createdBy.length < 1) errors["Nickname"] = "cannot be empty";
    if (formData.expiry < minDate || formData.expiry > maxDate) errors["Expiry"] = "must be greater than 30 minutes and less than 30 days";


    setFormData(prevState => ({
      ...prevState,
      errors: errors
    }))

    let numErrors = Object.keys(errors).length;

    if (numErrors === 0) {
      let data: AcceptedCreateListFormData = {
        createdBy: formData.createdBy,
        title: formData.title,
        description: formData.description,
        passphrase: formData.passphrase,
        expiry: formData.expiry.toISOString().slice(0, 19) + "Z",
      };
      await sendApiRequest("POST", data)
      .then(res => res.json())
      .then(data => console.log(data))
      .catch(err => console.log(err));      
    }
  };

  const handleInputChange = (e: ChangeEvent<HTMLInputElement>) => {
    handleFormChange(e.currentTarget.id, e.currentTarget.value);
  };

  const handleTextareaChange = (e: ChangeEvent<HTMLTextAreaElement>) => {
    handleFormChange(e.currentTarget.id, e.currentTarget.value);
  };

  const handleFormChange = (name: string, value: any) => {
    if (name == "expiry") {
      value = new Date(value);
    }
    else { 
      value = value;
    }
    setFormData({...formData, [name]: value});
  }; 

  return (
    <> 
      {
        showModal ? (
          <div id="create-list-base-modal" className="base-modal" onClick={toggleShowModal}> 
            <CreateListModalContent toggleShowModal={toggleShowModal} createListFormSubmit={createListFormSubmit} formData={formData} handleInputChange={handleInputChange} handleTextareaChange={handleTextareaChange}/>
          </div>
        ): null
      }
    </>
  )
}