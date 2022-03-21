// Include standard headers
#include <stdio.h>
#include <stdlib.h>
// Include GLEW
#include <GL/glew.h>

// Include GLFW
#include <GLFW/glfw3.h>
GLFWwindow* window;

// Include GLM
#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>
#include <glm/gtx/transform.hpp>
using namespace glm;

#include <common/shader.hpp>

void rotate_y(float angle, glm::vec3 &rotated_vector) {
	glm::mat3 rotation_matrix = {
		cos(angle), 0, sin(angle),
		0, 1, 0,
		-sin(angle), 0, 1
	};
	rotated_vector = rotation_matrix * rotated_vector;
}

int main(void)
{
	// Initialise GLFW
	if (!glfwInit())
	{
		fprintf(stderr, "Failed to initialize GLFW\n");
		getchar();
		return -1;
	}

	glfwWindowHint(GLFW_SAMPLES, 4);
	glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, 3);
	glfwWindowHint(GLFW_CONTEXT_VERSION_MINOR, 3);
	glfwWindowHint(GLFW_OPENGL_FORWARD_COMPAT, GL_TRUE); // To make MacOS happy; should not be needed
	glfwWindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_CORE_PROFILE);

	// Open a window and create its OpenGL context
	window = glfwCreateWindow(1024, 768, "Tutorial 02 - Red triangle", NULL, NULL);
	if (window == NULL) {
		fprintf(stderr, "Failed to open GLFW window. If you have an Intel GPU, they are not 3.3 compatible. Try the 2.1 version of the tutorials.\n");
		getchar();
		glfwTerminate();
		return -1;
	}
	glfwMakeContextCurrent(window);

	// Initialize GLEW
	glewExperimental = true; // Needed for core profile
	if (glewInit() != GLEW_OK) {
		fprintf(stderr, "Failed to initialize GLEW\n");
		getchar();
		glfwTerminate();
		return -1;
	}

	// Ensure we can capture the escape key being pressed below
	glfwSetInputMode(window, GLFW_STICKY_KEYS, GL_TRUE);

	// Dark blue background
	glClearColor(0.0f, 0.0f, 0.0f, 0.0f);

	// Включить тест глубины
	glEnable(GL_DEPTH_TEST);
	// Фрагмент будет выводиться только в том, случае, если он находится ближе к камере, чем предыдущий
	glDepthFunc(GL_LESS);

	GLuint VertexArrayID;


	glGenVertexArrays(1, &VertexArrayID);
	glBindVertexArray(VertexArrayID);

	// Create and compile our GLSL program from the shaders
	GLuint RedProgramID = LoadShaders("SimpleVertexShader.vertexshader", "RedFragmentShader.fragmentshader");
	GLuint GreenProgramID = LoadShaders("SimpleVertexShader.vertexshader", "GreenFragmentShader.fragmentshader");

	// Get a handle for our "MVP" uniform
	GLuint RedMatrixID = glGetUniformLocation(RedProgramID, "MVP");
	GLuint GreenMatrixID = glGetUniformLocation(GreenProgramID, "MVP");

	static const GLfloat g_red_vertex_buffer_data[] = {
		-1.0f, -1.0f, 0.0f,
		 1.0f, -1.0f, 0.0f,
		 0.0f,  1.0f, 0.0f,
	};

	static const GLfloat g_green_vertex_buffer_data[] = {
		-1.0f,  1.0f,  1.0f,
		 1.0f,  1.0f,  0.0f,
		 0.0f, -1.0f, -1.0f,
	};

	GLuint redvertexbuffer;
	glGenBuffers(1, &redvertexbuffer);
	glBindBuffer(GL_ARRAY_BUFFER, redvertexbuffer);
	glBufferData(GL_ARRAY_BUFFER, sizeof(g_red_vertex_buffer_data), g_red_vertex_buffer_data, GL_STATIC_DRAW);

	GLuint greenvertexbuffer;
	glGenBuffers(1, &greenvertexbuffer);
	glBindBuffer(GL_ARRAY_BUFFER, greenvertexbuffer);
	glBufferData(GL_ARRAY_BUFFER, sizeof(g_red_vertex_buffer_data), g_green_vertex_buffer_data, GL_STATIC_DRAW);

	// Projection matrix : 45° Field of View, 4:3 ratio, display range : 0.1 unit <-> 100 units
	glm::mat4 Projection = glm::perspective(glm::radians(45.0f), 4.0f / 3.0f, 0.1f, 100.0f);
	// Model matrix : an identity matrix (model will be at the origin)
	glm::mat4 Model = glm::mat4(1.0f);

	glm::vec3 CameraPosition = glm::vec3(0, 0, -3);
	float rotation_speed = 1;

	double lastTime = glfwGetTime();

	glEnable(GL_BLEND);
	glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);

	do {

		// Clear the screen
		glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

		// Compute delta time
		double currentTime = glfwGetTime();
		float deltaTime = float(currentTime - lastTime);
		lastTime = currentTime;

		rotate_y(rotation_speed * deltaTime, CameraPosition);
		// Camera matrix
		glm::mat4 View = glm::lookAt(
			CameraPosition, // Camera is at CameraPosition, in World Space
			glm::vec3(0, 0, 0), // and looks at the origin
			glm::vec3(0, 1, 0)  // Head is up (set to 0,-1,0 to look upside-down)
		);
		glm::mat4 MVP = Projection * View * Model; // Remember, matrix multiplication is the other way around


		// Use red shader
		glUseProgram(RedProgramID);

		// Send our transformation to the currently bound shader, 
		// in the "MVP" uniform
		glUniformMatrix4fv(RedMatrixID, 1, GL_FALSE, &MVP[0][0]);

		// 1rst attribute buffer : vertices
		glEnableVertexAttribArray(0);
		glBindBuffer(GL_ARRAY_BUFFER, redvertexbuffer);
		glVertexAttribPointer(
			0,                  // attribute 0. No particular reason for 0, but must match the layout in the shader.
			3,                  // size
			GL_FLOAT,           // type
			GL_FALSE,           // normalized?
			0,                  // stride
			(void*)0            // array buffer offset
		);

		// Draw the red triangle !
		glDrawArrays(GL_TRIANGLES, 0, 3); // 3 indices starting at 0 -> 1 triangle

		glDisableVertexAttribArray(0);


		// Use green shader
		glUseProgram(GreenProgramID);

		// Send our transformation to the currently bound shader, 
		// in the "MVP" uniform
		glUniformMatrix4fv(GreenMatrixID, 1, GL_FALSE, &MVP[0][0]);

		// 1rst attribute buffer : vertices
		glEnableVertexAttribArray(0);
		glBindBuffer(GL_ARRAY_BUFFER, greenvertexbuffer);
		glVertexAttribPointer(
			0,                  // attribute 0. No particular reason for 0, but must match the layout in the shader.
			3,                  // size
			GL_FLOAT,           // type
			GL_FALSE,           // normalized?
			0,                  // stride
			(void*)0            // array buffer offset
		);

		// Draw the green triangle !
		glDrawArrays(GL_TRIANGLES, 0, 3); // 3 indices starting at 0 -> 1 triangle

		glDisableVertexAttribArray(0);


		// Swap buffers
		glfwSwapBuffers(window);
		glfwPollEvents();

	} // Check if the ESC key was pressed or the window was closed
	while (glfwGetKey(window, GLFW_KEY_ESCAPE) != GLFW_PRESS &&
		glfwWindowShouldClose(window) == 0);

	// Cleanup VBO
	glDeleteBuffers(1, &redvertexbuffer);
	glDeleteBuffers(1, &greenvertexbuffer);
	glDeleteVertexArrays(1, &VertexArrayID);
	glDeleteProgram(RedProgramID);
	glDeleteProgram(GreenProgramID);

	// Close OpenGL window and terminate GLFW
	glfwTerminate();

	return 0;
}
