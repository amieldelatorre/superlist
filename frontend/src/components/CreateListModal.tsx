import { FC, ChangeEvent, useState, useEffect, useCallback } from "react";
import '../css/main.css';

interface CreateListModalProps {
  showModal: boolean;
  toggleShowModal(): void;
}

type FormData = {
  createdBy: string;
  title: string;
  description: string;
  passphrase: string;
  expiry: Date;
};

export const CreateListModal: FC<CreateListModalProps> = ({showModal, toggleShowModal}: CreateListModalProps) => {
  const [formData, setFormData] = useState<FormData>({createdBy: "", title: "", description: "", passphrase: "", expiry: new Date(Date.now() + (3600 * 1000 * 24))});
  
  const handleEscKey = useCallback((e: KeyboardEvent) => {
    if (e.key === "Escape") {
      toggleShowModal();
    }
  }, [toggleShowModal]);

  useEffect(() => {
    document.addEventListener('keyup', handleEscKey, false);
  });

  const createListFormSubmit = (e: React.FormEvent) => {
    console.log(formData);
    e.preventDefault()
    toggleShowModal();
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

  const dateToDateTimeLocalString = (date: Date): string => {
    const year = date.getFullYear().toString();
    const month = date.getMonth() < 10 ? `0${date.getMonth()}`: date.getMonth();
    const day = date.getDate() < 10 ? `0${date.getDate()}`: date.getDate();


    return [year, month, day].join('-') + "T" + date.toTimeString().slice(0, 5);
  } 

  return (
    <> 
      {
        showModal ? (
          <div id="create-list-base-modal" className="base-modal" onClick={toggleShowModal}> 
            <div className="create-list-modal-content" onClick={e => {e.stopPropagation();}}> 
              <div>
                <span className="close-button" onClick={toggleShowModal}>&times;</span>
              </div>
              <form onSubmit={createListFormSubmit}>
                <div className="form-field text-field-wrap">
                  <input type="text" id="title" value={formData.title} onChange={handleInputChange} required/>
                  <label htmlFor="title">Title:</label>
                </div>

                <div className="form-field text-field-wrap">
                  <input type="text" id="createdBy" value={formData.createdBy} onChange={handleInputChange} required/>
                  <label htmlFor="createdBy">Nickname:</label>
                </div>

                <div className="form-field textarea-field-wrap">
                  <textarea id="description"value={formData.description} onChange={handleTextareaChange} required/>
                  <label htmlFor="description">Description:</label>
                </div>

                <div className="form-field text-field-wrap">
                  <input type="password" id="passphrase" value={formData.passphrase} onChange={handleInputChange} required/>
                  <label htmlFor="passphrase">Passphrase:</label>
                </div>

                <div className="form-field datetime-local-field-wrap">
                  <label htmlFor="expiry">Expiry:</label>
                  <input type="datetime-local" id="expiry" value={dateToDateTimeLocalString(formData.expiry)} onChange={handleInputChange} required/>
                </div>
                
                <div className="create-list-form-submit-button-wrapper">
                  <button className="create-list-form-submit-button" type="submit">Create list</button>
                </div>
              </form>
            </div>
          </div>
        ): null
      }
    </>
  )
}