class ShoppingList extends React.Component{
    render(){
        return (
            <div className="shopping-list">
                <h1>Shopiing List for {this.props.name}</h1>
                <ul>
                    <li>Instagram</li>
                    <li>WhatsApp</li>
                    <li>Oculus</li>
                </ul>
            </div>
        );
    }
}

//sample usage <ShoppingList name="Mark" />