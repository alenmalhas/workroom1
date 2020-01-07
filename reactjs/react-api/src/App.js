import React, {Component} from 'react';
import Contacts from './components/Contacts';

class App extends Component{

  constructor(props){
    super(props);
    this.state = {
      contacts: []
    };
  }

  componentDidMount(){
    
  }

  setStateFromApiData(){

    if(this.state.contacts.length === 0) 
    {
      fetch('http://jsonplaceholder.typicode.com/users')
      .then(res => res.json())
      .then(data => {
        this.setState({contacts: data})
      })
      .catch(console.log)
    }
    else{
      alert('Contacts already loaded');
    }
  }

  clearContactList(){
    this.setState({
      contacts: []
    });
  }



// <div class="card">
//   <div class="card-body">
//     <h5 class="card-title">Steve Jobs</h5>
//     <h6 class="card-subtitle mb-2 text-muted">steve@apple.com</h6>
//     <p class="card-text">Stay Hungry, Stay Foolish</p>
//   </div>
// </div>

  render(){
    return(
      <div>
        <input type="button" onClick={()=> this.setStateFromApiData()} value="Load Contacts" />
        <input type="button" onClick={()=> this.clearContactList() } value="Clear" />
        <Contacts contactList={this.state.contacts} />
      </div>
    );
  }
}

export default App;

