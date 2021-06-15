import React from "react";
import { connect } from "react-redux";
import { UserPanel } from "./UserPanel"
import { url } from "../commons/properties";
import { Row, Form, Button, Container, InputGroup, FormControl } from "react-bootstrap";

export class UserList extends React.Component {
    constructor(props) {
        super(props);
        this.state =
        {
            email: '',
            type: this.props.type
        };

        this.handleChange = this.handleChange.bind(this);
        this.loadUserList = this.loadUserList.bind(this);
    }

    async handleChange(event) {
        this.setState({ email: event.target.value });
        /*  const email = this.state.email;
          const type = this.state.type;
          let type2= "specialist";
          if (type ==="specialist"){
              type2="customer" 
          }
          const response = await fetch(url + '/' + type2 + 's?email=' + email);
          const userList = await response.json();
          this.props.dispatch({ type: "LOAD_USER_LIST", userList: userList }); */

    }

    async loadUserList(event) {
        event.preventDefault();
        const email = this.state.email;
        const type = this.state.type;
        let type2 = "specialist";
        if (type === "specialist") {
            type2 = "customer"
        }
        const response = await fetch(url + '/' + type2 + 's?email=' + email);
        const userList = await response.json();
        this.props.dispatch({ type: "LOAD_USER_LIST", userList: userList });

    }

    canLoad() {
        if (!this.props.userData)
            return false
        if (!this.props.userList || this.props.userList.length === 0)
            return false


        return true
    }


    render() {
        const userList = this.props.userList;
        const userData = this.props.userData;
        const handler = this.props.handler;

        let type = this.props.type;
        return (
            <Container>
                <Row>
                    <Form onSubmit={this.loadUserList}>
                        <Form.Group>
                            <InputGroup>
                                <FormControl
                                    value={this.state.value}
                                    onChange={this.handleChange}
                                    placeholder="E-mail"
                                    aria-label="E-mail"
                                    aria-describedby="basic-addon2"
                                />
                                <InputGroup.Append>
                                    <Button variant="outline-secondary" type="submit" value="Szukaj">Szukaj</Button>
                                </InputGroup.Append>
                            </InputGroup>
                        </Form.Group>
                    </Form>
                    {/* <form onSubmit={this.loadUserList}>
                    <label>
                    <input type="text" value={this.state.value} onChange={this.handleChange} placeholder="Email"/>
                    </label>
                    <input type="submit" value="Szukaj" />
                </form> */}
                </Row>
                <Row>
                    <ul class="list-group">
                        {this.canLoad() === false ? (
                            <h3>...</h3>
                        ) :
                            userList.map(function (user, idx) {
                                if (type === "specialist") {
                                    return <UserPanel key={idx} type="specialist" id={userData.id} userId={user.id} userName={user.name}
                                        userSurname={user.surname} userRole={userData.role} userEmail={user.email} handler={handler} />
                                } else {
                                    return <UserPanel key={idx} type="customer" id={userData.id} userId={user.id} userName={user.name}
                                        userSurname={user.surname} userRole={user.role} userEmail={user.email} handler={handler} />
                                }
                            })
                        }
                    </ul>
                </Row>
            </Container>
        )

    }
}

const mapStateToProps = (state) => {
    return {
        userData: state.userData,
        userList: state.userList
    };
};

UserList = connect(mapStateToProps)(UserList);