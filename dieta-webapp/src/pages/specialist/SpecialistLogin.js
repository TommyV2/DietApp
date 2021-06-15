import React, { Fragment } from "react";
import { Form, Button, Col, Row, Container, Jumbotron } from "react-bootstrap";
import { url } from "../../commons/properties";
import { Alert, Spinner } from "reactstrap"

export class SpecialistLogin extends React.Component {
    constructor(props){
        super(props);
        this.emailRef = React.createRef();
        this.passwordRef = React.createRef();
        this.state = {
            errorMsg: "",
            loading: false
        }
    }

    handleSubmit = (event) => {
        event.preventDefault();
        this.setState({
            loading: true
        })
        const email = this.emailRef.current.value;
        const password = this.passwordRef.current.value;
        const data = JSON.stringify({
            email: email,
            password: password,
            accountRole: "specialist"
        });

        if(email === "" || password === ""){
            this.setState({
                errorMsg: "Wszystkie pola są wymagane!",
                loading: false
            });
            return
        }

        const requestOptions = {
            method: 'POST',
            headers: { "Content-Type": "application/json; charset=UTF-8"},
            body: data
          }
          fetch(url + '/account/login', requestOptions)
          .then(async response => {
                const data = await response.json();
                if (!response.ok) {
                  const error = (data && data.error) || response.statusText;
                  return Promise.reject(error);
                }
                // TODO
                this.setState({
                    errorMsg: "",
                    loading: false
                });
                document.cookie = "specialistId="+data.specialistId+";path=/;";
                window.location.href = '/specialist/dashboard';
              })
              .catch(error => {
                // TODO
                var message;
                if (error === 'Bad Request') {
                    message = 'Login i/lub hasło są niepoprawne.';
                }
                else {
                    message = 'Problem z połączeniem. Spróbuj ponownie póżniej.'
                }
                this.setState({
                    errorMsg: message,
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
                                <Form.Group controlId="email">
                                    <Form.Label>E-mail</Form.Label>
                                    <Form.Control type="text" ref={this.emailRef} placeholder="E-mail" />
                                </Form.Group>

                                <Form.Group controlId="password">
                                    <Form.Label>Hasło</Form.Label>
                                    <Form.Control type="password" ref={this.passwordRef} placeholder="Hasło" />
                                </Form.Group>
                                <Button variant="primary" type="submit" onClick={this.handleSubmit}>
                                Zaloguj się
                            </Button>
                                {/* <LinkContainer to="/specialist/dashboard">
                                    <Button variant="primary">
                                        Zaloguj się
                                    </Button>
                                </LinkContainer>
                                {/* <Button variant="primary" onClick={this.handleClick}>
                                    Zaloguj się
                                </Button> */}
                            </Form>
                        </Jumbotron>
                    </Col>
                </Row>
            </Container>
        );
    }
}