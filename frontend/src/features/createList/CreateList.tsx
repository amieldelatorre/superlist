import { FC, ChangeEvent, useState, useEffect, useCallback, ReactElement } from "react";
import { AcceptedCreateListFormData, sendApiRequest } from '../../api/api';

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

  const dateToDateTimeLocalString = (date: Date): string => {
    const year = date.getFullYear().toString();
    const month = date.getMonth() < 10 ? `0${date.getMonth() + 1}`: date.getMonth() + 1;
    const day = date.getDate() < 10 ? `0${date.getDate()}`: date.getDate();


    return [year, month, day].join('-') + "T" + date.toTimeString().slice(0, 5);
  } 

  const getErrors = (): ReactElement => {
    return (
      <div className="form-errors">
        {
          Object.entries(formData.errors)
            .map( ([key, value]) => 
              <li className="red-text form-errors-text" key={key}><em>{key}: {value}</em></li>
            )
        }
      </div>
    )
  }

  return (
    <> 
      {
        showModal ? (
          <div id="create-list-base-modal" className="base-modal" onClick={toggleShowModal}> 
            <div className="create-list-modal-content" onClick={e => {e.stopPropagation();}}> 
              <div className="form-header">
                <h1 className="form-title">Create a List</h1>
                <button className="close-button" onClick={toggleShowModal}>&times;</button>
              </div>
              
              <form className="create-list-form" onSubmit={createListFormSubmit}>
                {getErrors()}

                <div className="form-field text-field-wrap">
                  <input type="text" id="title" value={formData.title} onChange={handleInputChange} required/>
                  <label htmlFor="title">
                    Title
                    <i className="custom-fa tooltip fa">&#xf059;
                      <span className="tooltip-text">Give this list a name</span>
                    </i>
                  </label>
                </div>

                <div className="form-field text-field-wrap">
                  <input type="text" id="createdBy" value={formData.createdBy} onChange={handleInputChange} required/>
                  <label htmlFor="createdBy">
                    Nickname
                    <i className="custom-fa tooltip fa">&#xf059;
                      <span className="tooltip-text">Nickname to show who created this list. Whatever you want to be called by!</span>
                    </i>
                  </label>
                </div>

                <div className="form-field textarea-field-wrap">
                  <textarea id="description"value={formData.description} onChange={handleTextareaChange} required/>
                  <label htmlFor="description">
                    Description
                    <i className="custom-fa tooltip fa">&#xf059;
                      <span className="tooltip-text">Describe what this list is about</span>
                    </i>
                  </label>
                </div>

                <div className="form-field text-field-wrap">
                  <input type="password" id="passphrase" value={formData.passphrase} onChange={handleInputChange} required/>
                  <label htmlFor="passphrase">
                    Passphrase
                    <i className="custom-fa tooltip fa">&#xf059;
                      <span className="tooltip-text">Shared secret to be given to others who will view this list. This is <strong className="red-text">NOT</strong> a secure password that only you will know!</span>
                    </i>
                  </label>
                </div>

                <div className="form-field datetime-local-field-wrap">
                  <label htmlFor="expiry">
                    Expiry
                    <i className="custom-fa tooltip fa">&#xf059;
                      <span className="tooltip-text">When the the list will be deleted</span>
                    </i>
                  </label>
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