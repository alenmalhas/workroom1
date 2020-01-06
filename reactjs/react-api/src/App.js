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
    fetch('http://jsonplaceholder.typicode.com/users')
    .then(res => res.json())
    .then(data => {
      this.setState({contacts: data})
    })
    .catch(console.log)

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
      <Contacts contactList={this.state.contacts} />
    );
  }
}

export default App;

