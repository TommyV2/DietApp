import React, {Fragment} from "react";
import { Form, Button, Col, Row, Container, Jumbotron } from "react-bootstrap";
import { url } from "../../commons/properties";
import {Alert, Spinner} from "reactstrap";
import {validateEmail} from "../../commons/validation/validateEmail";

export class SpecialistRegister extends React.Component {
    constructor(props){
        super(props);
        this.nameRef = React.createRef();
        this.surnameRef = React.createRef();
        this.emailRef = React.createRef();
        this.passwordRef = React.createRef();
        this.password2Ref = React.createRef();
        this.state = {
            errorMsg: "",
            successMsg: "",
            loading: false
        }
    }

    clearInputFields(){
        this.nameRef.current.value = "";
        this.surnameRef.current.value = "";
        this.emailRef.current.value = "";
        this.passwordRef.current.value = "";
        this.password2Ref.current.value = "";
    }

    handleSubmit = (event) => {
        event.preventDefault();
        this.setState({
            loading: true
        })
        const name = this.nameRef.current.value;
        const surname = this.surnameRef.current.value;
        const email = this.emailRef.current.value.toLowerCase();
        const password = this.passwordRef.current.value;
        const password2 = this.password2Ref.current.value;

        if(name === "" || surname === "" || email === "" || password === "" || password2 === ""){
            this.setState({
                errorMsg: "Wszystkie pola są wymagane!",
                successMsg: "",
                loading: false
            });
            return
        }

        if(!validateEmail(email)){
            this.setState({
                errorMsg: "Niepoprawny adres email!",
                successMsg: "",
                loading: false
            });
            return
        }

        if(password !== password2){
            //TODO
            this.setState({
                errorMsg: "Hasła są różne",
                successMsg: "",
                loading: false
            });
            return
        }

        const data = JSON.stringify({
            name: name,
            surname: surname,
            email: email,
            password: password,
            accountRole: "specialist"
        });

        const requestOptions = {
            method: 'POST',
            headers: { "Content-Type": "application/json; charset=UTF-8"},
            body: data
          }
          fetch(url + '/account/register', requestOptions)
          .then(async response => {
                if (!response.ok) {
                  const error = response.statusText;
                  return Promise.reject(error);
                }
                //TODO
                this.setState({
                    errorMsg: "",
                    successMsg: "Zarejestrowano pomyślnie! Zaloguj się, " + name + ".",
                    loading: false
                });
                this.clearInputFields();
              })
              .catch(error => {
                // TODO 

                this.setState({
                    // errorMsg: error.toString(),
                    errorMsg: "Rejestracja nie powiodła się.",
                    successMsg: "",
                    loading: false
                });
            });
        }

    render() {
        return (
            <Container>
                <Row className="justify-content-md-center">
                    <Col md="auto">
                        <Jumbotron>
                            <Form>
                                {this.state.loading ? <Spinner size="sm" type="grow" color="primary" />:<Fragment/>}
                                {this.state.errorMsg === ""?<Fragment/>:<Alert color="danger">{this.state.errorMsg}</Alert>}
                                {this.state.successMsg === ""?<Fragment/>:<Alert color="success">{this.state.successMsg}</Alert>}
                                <Form.Group controlId="name">
                                    <Form.Label>Imię</Form.Label>
                                    <Form.Control type="text" ref={this.nameRef} placeholder="Imię" />
                                </Form.Group>

                                <Form.Group controlId="surname">
                                    <Form.Label>Nazwisko</Form.Label>
                                    <Form.Control type="text" ref={this.surnameRef} placeholder="Nazwisko" />
                                </Form.Group>

                                <Form.Group controlId="email">
                                    <Form.Label>E-mail</Form.Label>
                                    <Form.Control type="text" ref={this.emailRef} placeholder="E-mail" />
                                </Form.Group>

                                <Form.Group controlId="password">
                                    <Form.Label>Hasło</Form.Label>
                                    <Form.Control type="password" ref={this.passwordRef} placeholder="Hasło" />
                                </Form.Group>

                                <Form.Group controlId="password2">
                                    <Form.Label>Powtórz hasło</Form.Label>
                                    <Form.Control type="password" ref={this.password2Ref} placeholder="Powtórz hasło" />
                                </Form.Group>
                                
                                <Button variant="primary" onClick={this.handleSubmit}>
                                    Zarejestruj się
                                </Button>
                            </Form>
                        </Jumbotron>
                    </Col>
                </Row>
            </Container>
        );
    }
}