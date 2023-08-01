import { FC, ReactElement, ChangeEventHandler } from 'react';
import { CreateListFormData } from './CreateListModal';

type CreateListModalContentProps = {
  toggleShowModal(): void;
  createListFormSubmit(e: React.FormEvent): Promise<void>;
  formData: CreateListFormData;
  handleInputChange: ChangeEventHandler;
  handleTextareaChange: ChangeEventHandler;

}

export const CreateListModalContent: FC<CreateListModalContentProps> = ({toggleShowModal, createListFormSubmit, formData, handleInputChange, handleTextareaChange}) => {
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

  const dateToDateTimeLocalString = (date: Date): string => {
    const year = date.getFullYear().toString();
    const month = date.getMonth() < 10 ? `0${date.getMonth() + 1}`: date.getMonth() + 1;
    const day = date.getDate() < 10 ? `0${date.getDate()}`: date.getDate();


    return [year, month, day].join('-') + "T" + date.toTimeString().slice(0, 5);
  }

  return (
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
  )
}